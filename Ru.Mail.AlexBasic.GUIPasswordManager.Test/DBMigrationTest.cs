using NUnit.Framework;
using Ru.Mail.AlexBasic.GUIPasswordManager;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ru.Mail.AlexBasic.GUIPasswordManager.Test
{
    public class DBMigrationTest
    {
        [SetUp]
        public void Setup() { }

        [Test]
        public void MigrationTest()
        {
            using (var context = new SqliteContext())
            {
                context.WithTransaction(context => new FirstMigration(context).Up());

                var (migrationName, dontExist) = new FirstMigration(context).GetLastMigration();

                Assert.AreEqual(nameof(FirstMigration), migrationName);
                Assert.IsFalse(dontExist);

                var groupId = context.ExecuteNonQuery(
                    "insert into SecretGroup (Name) values (@Name)", 
                    new { Name = "Group name" });
                var secretId = context.ExecuteNonQuery(
                    "insert into Secret (Name, Value, SecretGroupId) values (@Name, @Value, @SecretGroupId)", 
                    new { Name = "Name", 
                        Value = new byte[] {
                            (byte)'1', (byte)'2', (byte)'3', (byte)'4', (byte)'5' }, 
                        SecretGroupId = groupId });

                var secretGroups = context.ExecuteQuery<SecretGroup>(
                    $"SELECT Name FROM SecretGroup;");
                var secrets = context.ExecuteQuery<SecretGroup>(
                    $"SELECT Name FROM Secret;");

                Assert.AreEqual("Group name", secretGroups.First().Name);
                Assert.AreEqual("Name", secrets.First().Name);

                context.WithTransaction(context => new FirstMigration(context).Down());
            }
        }
    }
}