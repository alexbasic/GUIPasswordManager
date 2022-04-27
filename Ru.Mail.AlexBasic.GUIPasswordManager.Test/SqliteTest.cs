using NUnit.Framework;
using Ru.Mail.AlexBasic.GUIPasswordManager;
using Ru.Mail.AlexBasic.GUIPasswordManager.Infrastructure.Db;
using System;

namespace Ru.Mail.AlexBasic.GUIPasswordManager.Test
{
    public class SqliteTest
    {
        [SetUp]
        public void Setup() { }

        [Test]
        public void IsForeignKeysEnabled()
        {
            using (var context = new SqliteContext())
            {
                var r = context.ExecuteScalar<long>("PRAGMA foreign_keys;");

                if (r == 1) Console.WriteLine("foreign_keys is supported");
            }
        }
    }
}