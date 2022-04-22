using System;
using System.Security.Cryptography;
using System.Text;

namespace Ru.Mail.AlexBasic.GUIPasswordManager
{
    public class CryptoProvider
    {
        public byte[] Encode(string data, string password)
        {
            var entropy = GetEntropy(password);
            return ProtectedData.Protect(Encoding.UTF8.GetBytes(data), 
                entropy, DataProtectionScope.CurrentUser);

        }

        public string Decode(byte[] data, string password)
        {
            var entropy = GetEntropy(password);
            try
            {
                return Encoding.UTF8.GetString(ProtectedData.Unprotect(data,
                    entropy, DataProtectionScope.CurrentUser));
            }
            catch (CryptographicException e)
            {
                throw new CryptoProviderException(e);
            }
        }

        private byte[] GetEntropy(string password)
        {
            var md5 = MD5.Create();
            return md5.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }

    public class CryptoProviderException : Exception
    {
        public CryptoProviderException(Exception ex) : base("Не возможно расшифровать. Ошибка: " + ex.Message, ex) { }
    }
}
