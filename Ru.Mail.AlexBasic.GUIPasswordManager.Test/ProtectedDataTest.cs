using NUnit.Framework;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Ru.Mail.AlexBasic.GUIPasswordManager.Test
{
    public class ProtectedDataTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TheExampleOfProtection()
        {
            var data = Encoding.UTF8.GetBytes("The Example.");
            var entropy = CreateRandomEntropy();

            var protect = ProtectedData.Protect(data, entropy, DataProtectionScope.CurrentUser);
            var result = ProtectedData.Unprotect(protect, entropy, DataProtectionScope.CurrentUser);

            Assert.Pass();
        }

        [Test]
        public void ProtectionShouldThowExcwption()
        {
            var data = System.Text.Encoding.UTF8.GetBytes("The Example.");
            var entropy = CreateRandomEntropy();
            var anotherEntropy = new byte[] { 0, 1, 2, 3, 4, 5 };

            var protect = ProtectedData.Protect(data, entropy, DataProtectionScope.CurrentUser);
            Assert.Throws<CryptographicException>(() =>
            {
                try
                {
                    ProtectedData.Unprotect(protect, anotherEntropy, DataProtectionScope.CurrentUser);
                }
                //The exception proxy
                catch (Exception ex)
                {
                    if (typeof(CryptographicException).IsAssignableFrom(ex.GetType()))
                        throw new CryptographicException(ex.Message);
                    else
                        throw ex;
                }
            });
        }

        public byte[] CreateRandomEntropy()
        {
            var entropy = new byte[16];
            //fill with random value
            new RNGCryptoServiceProvider().GetBytes(entropy);
            return entropy;
        }
    }
}