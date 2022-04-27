using Ru.Mail.AlexBasic.GUIPasswordManager.Infrastructure.Db;
using Ru.Mail.AlexBasic.GUIPasswordManager.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Ru.Mail.AlexBasic.GUIPasswordManager.Domain
{
    public class SecretsProvider : ISecretsProvider
    {
        private readonly ISqliteContext _context;

        public SecretsProvider(ISqliteContext context)
        {
            _context = context;
        }

        public int AddSecret(Secret entity)
        {
            return _context.ExecuteNonQuery(
                "insert into Secret (Name, Comment, SecretGroupId, Value, Protected) values (@Name, @Comment, @SecretGroupId, @Value, @Protected)", 
                entity);
        }

        public int AddSecretGroup(SecretGroup entity)
        {
            return _context.ExecuteNonQuery(
                "insert into SecretGroup (Name, Comment) values (@Name, @Comment)",
                entity);
        }

        public IEnumerable<SecretGroup> GetAllSecretsGroups()
        {
            return _context.ExecuteQuery<SecretGroup>(
                "select Id, Name, Comment from SecretGroup");
        }

        public IEnumerable<Secret> GetAllSecrets()
        {
            return _context.ExecuteQuery<Secret>(
                "select Id, Name, Comment, SecretGroupId, Value, Protected from Secret");
        }

        public Secret GetSecret(int id)
        {
            return _context.ExecuteQuery<Secret>(
                "select Id, Name, Comment, SecretGroupId, Value, Protected from Secret where Id = @id limit 1", new { id })
                .FirstOrDefault();
        }

        public SecretGroup GetSecretGroup(int id)
        {
            return _context.ExecuteQuery<SecretGroup>(
                "select Id, Name, Comment from SecretGroup where Id = @id limit 1", new { id })
                .FirstOrDefault();
        }

        public IEnumerable<Secret> GetSecretsByGroupId(int groupId)
        {
            return _context.ExecuteQuery<Secret>(
                "select Id, Name, Comment, SecretGroupId, Value, Protected from Secret where SecretGroupId = @groupId limit 1", 
                new { groupId });
        }

        public void DeleteSecret(int id)
        {
            var count = _context.ExecuteNonQuery(
                "delete from Secret where id = @id", new{ id });
            if (count <= 0) throw new Exception("Didn't deleted secret");
        }

        public void DeleteSecretGroup(int id)
        {
            var count = _context.ExecuteNonQuery(
                "delete from SecretGroup where id = @id", new { id });
            if (count <= 0) throw new Exception("Didn't deleted group"); 
        }
    }
}
