using Microsoft.Data.Sqlite;
using Ru.Mail.AlexBasic.GUIPasswordManager.Controls;
using Ru.Mail.AlexBasic.GUIPasswordManager.Domain;
using Ru.Mail.AlexBasic.GUIPasswordManager.ViewModels;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ru.Mail.AlexBasic.GUIPasswordManager.Forms
{
    public partial class GUIPasswordForm : Form
    {
        private readonly MainManagerViewModel _viewModel;
        private readonly string _passwordStub;

        public GUIPasswordForm(ISecretsProvider secretsProvider)
        {
            InitializeComponent();

            using (var textBox = new TextBox() { UseSystemPasswordChar = true })
            {
                var _passwordChar = textBox.PasswordChar;
                _passwordStub = new string(new char[] { _passwordChar, _passwordChar, _passwordChar });
            }

            _viewModel = new MainManagerViewModel(Handle, secretsProvider);

            _viewModel.GroupsChanged += OnGroupsChanged;
            _viewModel.SecretsChanged += OnSecretsChanged;
            _viewModel.PasswordPromt = GetPassword;
            _viewModel.AferFoundLastWindow += OnAfterFoundLastWindow;
        }

        private void OnGroupsChanged(object sender, EventArgs e)
        {
            FillList();
        }

        private void OnSecretsChanged(object sender, EventArgs e)
        {
            FillList();
        }

        private void OnAfterFoundLastWindow(object sender, EventArgs e)
        {
            selectedWindowTextBox.Text = _viewModel.LastWindowName;
        }

        private (string value, bool success) GetPassword()
        {
            using (var passwordForm = new QueryPasswordForm())
            {
                var dialogResult = passwordForm.ShowDialog(this);
                if (dialogResult != DialogResult.OK) return (null, true);

                var keyCode = passwordForm.textBox1.Text;
                if (string.IsNullOrWhiteSpace(keyCode))
                {
                    MessageBox.Show("Пароль не может быть пустым");
                    return (null, false);
                }
                return (keyCode, true);
            }
        }

        private (string name, string comment, bool success) GetGroupParameters()
        {
            using (var form = new AddGroupForm())
            {
                var dialogResult = form.ShowDialog(this);
                if (dialogResult != DialogResult.OK) return (null, null, false);

                var name = form.nameTextBox.Text;
                var comment = form.commentTextBox.Text;
                if (string.IsNullOrWhiteSpace(name))
                {
                    MessageBox.Show("Нужно указать имя группы");
                    return (null, null, false);
                }
                return (name, comment, true);
            }
        }

        private (string name, string value, string comment, int secretGroupId, bool isPassword, bool success) GetSecretParameters()
        {
            using (var form = new AddSecretForm(_viewModel.SecretGroups.Select(x => 
                new AddSecretForm.SecretGroupValue { Id = x.Id, Name = x.Name }).ToList()))
            {
                var dialogResult = form.ShowDialog(this);
                if (dialogResult != DialogResult.OK) return (null, null, null, 0, false, false);

                var name = form.nameTextBox.Text;
                var value = form.valueTextBox.Text;
                var comment = form.commentTextBox.Text;
                var isPassword = form.isPasswordCheckBox.Checked;
                var secretGroupId = (int?)form.groupComboBox.SelectedValue ?? 0;
                if (string.IsNullOrWhiteSpace(name))
                {
                    MessageBox.Show("Нужно указать имя секрета");
                    return (null, null, null, 0, false, false);
                }
                if (string.IsNullOrWhiteSpace(value))
                {
                    MessageBox.Show("Нужно указать значение секрета");
                    return (null, null, null, 0, false, false);
                }
                if (string.IsNullOrWhiteSpace(value))
                {
                    MessageBox.Show("Нужно указать группу секрета");
                    return (null, null, null, 0, false, false);
                }
                return (name, value, comment, secretGroupId, isPassword, true);
            }
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            _viewModel.FindLastWindow();
            FillList();
        }

        private void FillList()
        {
            var groups = _viewModel.SecretGroups.Select(x => new ListViewAdd.GroupListView(x.Id, x.Name));
            listView1.SetValues(
                _viewModel.Secrets.Select(x => new ListViewAdd.SecretsListItem(
                    x.Id,
                    x.Name,
                    x.Protected ? _passwordStub : Encoding.UTF8.GetString(x.Value),
                    x.Comment,
                    x.SecretGroupId,
                    groups.First(g => g.Id == x.SecretGroupId).Name)),
                groups);
        }

        private void addGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var (name, comment, success) = GetGroupParameters();
            if (!success) return;
            _viewModel.AddGroup(name, comment);
        }

        private void addSecretToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var (name, value, comment, secretGroupId, isPassword, success) = GetSecretParameters();
            if (!success) return;
            _viewModel.AddSecret(name, value, comment, secretGroupId, isPassword);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var secretId = listView1.GetSelectedSecretId();
            if (secretId < 0)
            {
                MessageBox.Show("Сначала выберие секрет.");
                return;
            }
            _viewModel.SendSecretToForm(secretId);
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            button1_Click(sender, e);
        }

        private void deleteSecretToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var secretId = listView1.GetSelectedSecretId();
            if (secretId < 0)
            {
                MessageBox.Show("Сначала выберие секрет.");
                return;
            }
            if (MessageBox.Show("Do you sure deleting this secret?", "Deleting confirmation", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            _viewModel.DeleteSecret(secretId);
        }
    }
}
