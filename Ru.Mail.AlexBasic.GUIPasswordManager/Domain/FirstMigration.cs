using Ru.Mail.AlexBasic.GUIPasswordManager.Infrastructure.Db;
using System;

namespace Ru.Mail.AlexBasic.GUIPasswordManager.Domain
{
    public class FirstMigration : DBLiteMigration
    {
        public FirstMigration(ISqliteContext context) : base(context) { }

        public override void Up()
        {
            if (!NeedApplyMigration(nameof(FirstMigration))) return;

            EnableFireignKeys();

            AddMigration(nameof(FirstMigration));

            Context.ExecuteNonQuery(
@"CREATE TABLE if not exists SecretGroup 
(Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, 
Name TEXT NOT NULL, 
Comment TEXT NULL)");
            Context.ExecuteNonQuery(
@"CREATE TABLE if not exists Secret 
(Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, 
Name TEXT NOT NULL, 
Value BLOB, 
SecretGroupId INTEGER NOT NULL, 
Comment TEXT NULL,
Protected bool not null,
FOREIGN KEY(SecretGroupId) REFERENCES SecretGroup(Id))");
            Context.ExecuteNonQuery("insert into SecretGroup (Name, Comment) values (@Name, @Comment)",
                new 
                {
                    Name = "Default",
                    Comment = "This group for any secrets"
                });
        }

        public override void Down()
        {
            Context.ExecuteNonQuery("drop table if exists Secret");
            Context.ExecuteNonQuery("drop table if exists SecretGroup");

            DeleteMigration(nameof(FirstMigration));
        }
    }
}
