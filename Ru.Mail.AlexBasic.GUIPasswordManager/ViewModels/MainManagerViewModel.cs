using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Ru.Mail.AlexBasic.GUIPasswordManager
{
    public class MainManagerViewModel
    {
        private ISecretsProvider _secretsProvider;
        private readonly IntPtr _thisWindowHandle;

        public IntPtr LastWindowHandle { get; private set; }
        public string LastWindowName { get; private set; }
        private int _selectedGroup;
        public int SelectedGroup 
        { 
            get 
            {
                return _selectedGroup;
            } 
            set 
            {
                _selectedGroup = value;
                SecretsChanged?.Invoke(this, EventArgs.Empty);
            } 
        }
        public IEnumerable<SecretGroup> SecretGroups => _secretsProvider.GetAllSecretsGroups();
        public IEnumerable<Secret> Secrets => _secretsProvider.GetAllSecrets();

        public Func<(string, bool)> PasswordPromt;

        public event EventHandler SecretsChanged;
        public event EventHandler GroupsChanged;
        public event EventHandler AferFoundLastWindow;

        public MainManagerViewModel(IntPtr thisWindowHandle, ISecretsProvider secretsProvider)
        {
            _thisWindowHandle = thisWindowHandle;
            _secretsProvider = secretsProvider;
        }
        public void FindLastWindow()
        {
            LastWindowHandle = WinApi.GetLastWindow();
            LastWindowName = WinApi.GetWindowTitle(LastWindowHandle);
            AferFoundLastWindow?.Invoke(this, EventArgs.Empty);
        }

        public void AddGroup(string name, string comment)
        {
            _secretsProvider.AddSecretGroup(new SecretGroup { Name = name, Comment = comment });
            GroupsChanged?.Invoke(this, EventArgs.Empty);
        }

        public void AddSecret(string name, string value, string comment, int secretGroupId, bool isPassword)
        {
            var valueAsBytes = default(byte[]);
            if (isPassword)
            {
                var (password, success) = PasswordPromt();
                if (!success) return;
                valueAsBytes = new CryptoProvider().Encode(value, password);
            }
            else
            {
                valueAsBytes = Encoding.UTF8.GetBytes(value);
            }
            _secretsProvider.AddSecret(new Secret { Name = name, Comment = comment, SecretGroupId = secretGroupId, Value = valueAsBytes, Protected = isPassword });
            SecretsChanged?.Invoke(this, EventArgs.Empty);
        }

        public void DeleteGroup(int groupId)
        {
            _secretsProvider.DeleteSecretGroup(groupId);
            GroupsChanged?.Invoke(this, EventArgs.Empty);
        }

        public void DeleteSecret(int secretId)
        {
            _secretsProvider.DeleteSecret(secretId);
            SecretsChanged?.Invoke(this, EventArgs.Empty);
        }

        public void SendSecretToForm(int secretId)
        {
            var secret = _secretsProvider.GetSecret(secretId);
            var unprotected = string.Empty;
            if (secret.Protected)
            {
                var (password, success) = PasswordPromt();
                if (!success) return;
                try
                {
                    unprotected = new CryptoProvider().Decode(secret.Value, password);
                }
                catch (CryptoProviderException ex)
                {
                    MessageBox.Show(ex.Message + " Убедитесь, что пароль введен правильно!");
                    return;
                }
            }
            else
            {
                unprotected = Encoding.UTF8.GetString(secret.Value);
            }

            if (LastWindowHandle != _thisWindowHandle && LastWindowHandle != IntPtr.Zero)
            {
                WinApi.SetForegroundWindow(LastWindowHandle);
                SendKeys.SendWait(unprotected);
            }
        }
    }
}
