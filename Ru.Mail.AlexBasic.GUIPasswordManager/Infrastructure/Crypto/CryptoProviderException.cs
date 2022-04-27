using System;

namespace Ru.Mail.AlexBasic.GUIPasswordManager.Infrastructure.Crypto
{
    public class CryptoProviderException : Exception
    {
        public CryptoProviderException(Exception ex) : base("Не возможно расшифровать. Ошибка: " + ex.Message, ex) { }
    }
}
