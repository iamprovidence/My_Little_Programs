using Common.Structs;
using DisplayTextWorker.Domain.Parser;

namespace DisplayTextWorker.Domain.Builders
{
    public class FrBuilder : BuilderBase
    {
        public override LanguageCode LanguageCode => LanguageCode.French;

        protected override string FormatTicketCode(Token token)
        {
            return $"Votre code est {token.Value}";
        }

        protected override string FormatTicketNumber(Token token)
        {
            return $"Votre numéro est {token.Value}";
        }
    }
}
