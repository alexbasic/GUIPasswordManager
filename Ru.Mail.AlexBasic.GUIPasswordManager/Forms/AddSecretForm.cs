using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Ru.Mail.AlexBasic.GUIPasswordManager
{
    public partial class AddSecretForm : Form
    {
        public AddSecretForm(IEnumerable<SecretGroupValue> groups)
        {
            InitializeComponent();

            groupComboBox.DataSource = groups;
            groupComboBox.ValueMember = "Id";
            groupComboBox.DisplayMember = "Name";
        }

        public class SecretGroupValue
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        private void isPasswordCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            valueTextBox.UseSystemPasswordChar = isPasswordCheckBox.Checked;
        }
    }
}
