using UnityEngine.Localization.Tables;


namespace NeuroTranslate
{
    public sealed class Queue
    {
        public StringTable StringTable;
        public string Key;
        public string Language;

        public Queue (StringTable stringTable, string key, string language)
        {
            StringTable = stringTable;
            Key = key;
            Language = language;
        }

    }
}