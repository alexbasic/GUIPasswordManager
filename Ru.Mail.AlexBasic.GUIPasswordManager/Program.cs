using Ru.Mail.AlexBasic.GUIPasswordManager.Domain;
using Ru.Mail.AlexBasic.GUIPasswordManager.Forms;
using Ru.Mail.AlexBasic.GUIPasswordManager.Infrastructure.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ru.Mail.AlexBasic.GUIPasswordManager
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var context = new SqliteContext(dataSource: "data.db"))
            {
                context.WithTransaction(context => new FirstMigration(context).Up());

                var secretsProvider = new SecretsProvider(context);
                Application.Run(new GUIPasswordForm(secretsProvider));
            }
        }
    }
}
