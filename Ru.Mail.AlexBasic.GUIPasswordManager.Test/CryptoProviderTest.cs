using NUnit.Framework;
using Ru.Mail.AlexBasic.GUIPasswordManager.Infrastructure.Crypto;

namespace Ru.Mail.AlexBasic.GUIPasswordManager.Test
{
    public class CryptoProviderTest
    {
        [Test]
        public void TheExampleOfProtection()
        {
            var provider = new CryptoProvider();

            var stringForEncode = "Hello world! Hello world! Hello world! Hello world! Hello world!";
            var password = "somePassword";

            var encodedData = provider.Encode(stringForEncode, password);
            provider.Decode();

            Assert.Pass();
        }
    }
}