using Common.Structs;
using DisplayTextWorker.Domain.Parser;

namespace DisplayTextWorker.Domain.Builders
{
    public class EnBuilder : BuilderBase
    {
        public override LanguageCode LanguageCode => LanguageCode.English;

        protected override string FormatTicketCode(Token token)
        {
            return $"Your code is {token.Value}";
        }

        protected override string FormatTicketNumber(Token token)
        {
            return $"Your number is {token.Value}";
        }
    }
}
