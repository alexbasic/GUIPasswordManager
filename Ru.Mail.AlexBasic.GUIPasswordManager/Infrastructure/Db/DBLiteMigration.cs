using System;

namespace Ru.Mail.AlexBasic.GUIPasswordManager
{
    public abstract class DBLiteMigration
    {
        protected readonly ISqliteContext Context;

        public DBLiteMigration(ISqliteContext context)
        {
            Context = context;
        }

        public void EnableFireignKeys(bool enable = true) => Context.ExecuteNonQuery($"PRAGMA foreign_keys={(enable ? "on" : "off")};");

        public abstract void Up();

        public abstract void Down();

        public virtual void Seed()
        {
        }

        public virtual void UnSeed()
        {
        }

        public bool NeedApplyMigration(string name)
        {
            var (lastMigrationName, anyMigrationsDoestExist) = GetLastMigration();
            if (anyMigrationsDoestExist) return true;
            return lastMigrationName != name;
        }

        public (string name, bool anyMigrationsDoesntExist) GetLastMigration(bool failOnMigrationTableDoesntExist = false)
        {
            if (!MigrationTableIsExists())
                return (string.Empty, true);

            var name = Context.ExecuteScalar<string>(
                $"select Name from {(nameof(LiteMigration))} order by TimeStamp Desc limit 1;");
            return (name, false);
        }

        public void AddMigration(string name)
        {
            Context.ExecuteNonQuery(
@"CREATE TABLE if not exists " + nameof(LiteMigration) + @"
(Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, 
Timestamp INTEGER NOT NULL UNIQUE, 
Name TEXT NOT NULL)");
            var count = Context.ExecuteNonQuery(
                $"insert into {nameof(LiteMigration)} (TimeStamp, Name) values (@TimeStamp, @Name)",
                new { TimeStamp = DateTime.Now.Ticks, Name = name });
            if (count <= 0) throw new Exception("Migration wasn't added");
        }

        public void DeleteMigration(string name)
        {
            if (!MigrationTableIsExists()) throw new Exception("Migration don't exists");
            var count = Context.ExecuteNonQuery($"delete from {(nameof(LiteMigration))} WHERE name=@name;", new { name });
            if (count <= 0) throw new Exception("Migration wasn't deleted");
        }

        private bool MigrationTableIsExists() =>
            Context.ExecuteScalar<int>(
                $"SELECT count(1) FROM sqlite_master WHERE type='table' AND name=@Name;",
                new { Name = nameof(LiteMigration) }) > 0;
    }
}
