using NUnit.Framework;
using System.Security.Cryptography;
using System.Text;

namespace Ru.Mail.AlexBasic.GUIPasswordManager.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var data = System.Text.Encoding.UTF8.GetBytes("The Example.");
            var entropy = CreateRandomEntropy();

            var protect = default(byte[]);
            try
            {
                protect = ProtectedData.Protect(data, entropy, DataProtectionScope.CurrentUser);
            }
            catch (CryptographicException ex)
            {
                //ex.Message
            }

            var result = default(byte[]);
            try
            {
                result = ProtectedData.Unprotect(protect, entropy, DataProtectionScope.CurrentUser);
            }
            catch (CryptographicException ex)
            {
                //ex.Message
            }


            Assert.Pass();
        }

        public byte[] CreateRandomEntropy()
        {
            var entropy = new byte[16];
            //fill with random value
            new RNGCryptoServiceProvider().GetBytes(entropy);
            return entropy;
        }

        //public void ProtectMemoryTest()
        //{
        //    // Create the original data to be encrypted (The data length should be a multiple of 16).
        //    byte[] secret = { 1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4 };

        //    // Encrypt the data in memory. The result is stored in the same array as the original data.
        //    ProtectedMemory.Protect(secret, MemoryProtectionScope.SameLogon);

        //    // Decrypt the data in memory and store in the original array.
        //    ProtectedMemory.Unprotect(secret, MemoryProtectionScope.SameLogon);
        //}
    }
}