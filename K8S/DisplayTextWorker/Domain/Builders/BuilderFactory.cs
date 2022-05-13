using Common.Structs;

namespace DisplayTextWorker.Domain.Builders
{
    public static class BuilderFactory
    {
        public static BuilderBase GetBuilder(LanguageCode languageCode)
        {
            return languageCode switch
            {
                LanguageCode.English => new EnBuilder(),
                LanguageCode.French => new FrBuilder(),

                _ => new DefaultBuilder(languageCode),
            };
        }
    }
}
