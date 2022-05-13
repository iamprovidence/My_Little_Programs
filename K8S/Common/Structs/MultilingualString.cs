namespace Common.Structs
{
    public class MultilingualString : Dictionary<LanguageCode, string>
    {
        public string this[LanguageCode languageCode, LanguageCode defaultLanguageCode]
        {
            get
            {
                if (ContainsKey(languageCode))
                {
                    return base[languageCode];
                }

                return base[defaultLanguageCode];
            }
        }
    }
}
