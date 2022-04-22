namespace Ru.Mail.AlexBasic.GUIPasswordManager
{
    public class Secret : DictionaryEntity
    {
        public int SecretGroupId { get; set; }
        public byte[] Value { get; set; }
        public bool Protected { get; set; }
    }
}
