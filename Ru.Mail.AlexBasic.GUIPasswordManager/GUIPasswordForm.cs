using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ru.Mail.AlexBasic.GUIPasswordManager
{
    public partial class GUIPasswordForm : Form
    {
        private const string _configFileName = "data.mdf";

        public GUIPasswordForm()
        {
            InitializeComponent();

            if (File.Exists(_configFileName))
            {
                var protect = File.ReadAllBytes(_configFileName);

                var entropy = GetEntropy();
                if (entropy == null) return;
                var data = ProtectedData.Unprotect(protect, entropy, DataProtectionScope.CurrentUser);
                var json = Encoding.UTF8.GetString(data);
                var storage = JsonSerializer.Deserialize<MicroStorage>(json);
                textBox2.Text = storage.Login;
                textBox3.Text = storage.Password;
                textBox4.Text = storage.Domain;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SendText(textBox2.Text);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void SendText(string text)
        {
            var lastWindowHandle = WinApi.SetLastWindowForeground();
            if (lastWindowHandle != Handle && lastWindowHandle != IntPtr.Zero)
            {
                SendKeys.SendWait(text);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SendText(textBox3.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SendText(textBox4.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var entropy = GetEntropy();
            if (entropy == null) return;
            var storage = new MicroStorage 
            {
                Login = textBox2.Text,
                Password = textBox3.Text,
                Domain = textBox4.Text,
            };

            var json = JsonSerializer.Serialize(storage);
            var data = Encoding.UTF8.GetBytes(json);
            var protect = ProtectedData.Protect(data, entropy, DataProtectionScope.CurrentUser);
            File.WriteAllBytes(_configFileName, protect);
        }

        private string GetKeyCode()
        {
            using (var passwordForm = new QueryPasswordForm())
            {
                var dialogResult = passwordForm.ShowDialog(this);
                if (dialogResult != DialogResult.OK) return null;

                var keyCode = passwordForm.textBox1.Text;
                if (string.IsNullOrWhiteSpace(keyCode))
                {
                    MessageBox.Show("Пароль не может быть пустым");
                    return null;
                }
                return keyCode;
            }
        }

        private byte[] GetEntropy()
        {
            var keyCode = GetKeyCode();
            if (keyCode == null) return null;
            var md5 = MD5.Create();
            return md5.ComputeHash(Encoding.UTF8.GetBytes(keyCode));
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            var lastWindowHandle = WinApi.GetWindow(Process.GetCurrentProcess().MainWindowHandle, (uint)WinApi.GetWindow_Cmd.GW_HWNDNEXT);
            while (true)
            {
                IntPtr temp = WinApi.GetParent(lastWindowHandle);
                if (temp.Equals(IntPtr.Zero)) break;
                lastWindowHandle = temp;
            }
            textBox1.Text = $"{WinApi.GetWindowTitle(lastWindowHandle)} Handle: {lastWindowHandle}";
        }
    }

    public class MicroStorage
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Domain { get; set; }
    }
}
