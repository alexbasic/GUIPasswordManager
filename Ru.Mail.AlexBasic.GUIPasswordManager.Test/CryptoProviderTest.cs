using NUnit.Framework;
using Ru.Mail.AlexBasic.GUIPasswordManager.Infrastructure.Crypto;

namespace Ru.Mail.AlexBasic.GUIPasswordManager.Test
{
    public class CryptoProviderTest
    {
        [Test]
        public void ShouldEncodeAndDecode()
        {
            var provider = new CryptoProvider();

            var stringForEncode = "Hello world! Hello world! Hello world! Hello world! Hello world!";
            var password = "somePassword";

            var encodedData = provider.Encode(stringForEncode, password);
            var decodedData = provider.Decode(encodedData, password);

            Assert.AreEqual(stringForEncode, decodedData);
        }

        [Test]
        public void ShouldThowExceptionOnWrongPassword()
        {
            var provider = new CryptoProvider();

            var stringForEncode = 
                "Hello world! Hello world! Hello world! Hello world! Hello world!";
            var password = "somePassword";
            var anotherPassword = "12345"; 

            var encodedData = provider.Encode(stringForEncode, anotherPassword);

            Assert.Throws<CryptoProviderException>(() => 
                provider.Decode(encodedData, password));
        }
    }
}