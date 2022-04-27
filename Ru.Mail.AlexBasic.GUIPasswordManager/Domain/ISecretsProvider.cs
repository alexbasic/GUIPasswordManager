using Ru.Mail.AlexBasic.GUIPasswordManager.Model;
using System;
using System.Collections.Generic;

namespace Ru.Mail.AlexBasic.GUIPasswordManager.Domain
{
    public interface ISecretsProvider
    {
        public IEnumerable<SecretGroup> GetAllSecretsGroups();

        public IEnumerable<Secret> GetAllSecrets();

        public IEnumerable<Secret> GetSecretsByGroupId(int groupId);

        int AddSecretGroup(SecretGroup entity);

        int AddSecret(Secret entity);

        Secret GetSecret(int id);

        SecretGroup GetSecretGroup(int id);

        void DeleteSecret(int id);

        void DeleteSecretGroup(int id);
    }
}
