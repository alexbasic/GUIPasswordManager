using NUnit.Framework;
using Ru.Mail.AlexBasic.GUIPasswordManager.Forms;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Ru.Mail.AlexBasic.GUIPasswordManager.Test
{
    public class AddSecretFormTest
    {
        [Test]
        public void ShowForm()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AddSecretForm(new List<AddSecretForm.SecretGroupValue>
            {
                new AddSecretForm.SecretGroupValue 
                {
                    Id = 0,
                    Name = "Value 1"
                },
                new AddSecretForm.SecretGroupValue
                {
                    Id = 1,
                    Name = "Value 2"
                }
            }));
        }
    }
}