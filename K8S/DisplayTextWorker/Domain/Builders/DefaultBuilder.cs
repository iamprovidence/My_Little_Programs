using Common.Structs;

namespace DisplayTextWorker.Domain.Builders
{
    public class DefaultBuilder : BuilderBase
    {
        private readonly LanguageCode _languageCode;

        public override LanguageCode LanguageCode => _languageCode;

        public DefaultBuilder(LanguageCode languageCode)
        {
            _languageCode = languageCode;
        }
    }
}
