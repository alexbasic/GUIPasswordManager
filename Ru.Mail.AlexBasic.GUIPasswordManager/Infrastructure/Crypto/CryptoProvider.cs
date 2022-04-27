using System;
using System.Security.Cryptography;
using System.Text;

namespace Ru.Mail.AlexBasic.GUIPasswordManager.Infrastructure.Crypto
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
            catch (Exception ex)
            {
                if (typeof(CryptographicException).IsAssignableFrom(ex.GetType()))
                    throw new CryptoProviderException(ex);
                else
                    throw ex;
            }
        }

        private byte[] GetEntropy(string password)
        {
            var md5 = MD5.Create();
            return md5.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }
}
