using System.Text;
using Common.Structs;
using DisplayTextWorker.Domain.Parser;

namespace DisplayTextWorker.Domain.Builders
{
    public abstract class BuilderBase
    {
        public abstract LanguageCode LanguageCode { get; }

        public string Build(IEnumerable<Token> tokens)
        {
            var stringBuilder = new StringBuilder();
            foreach (var token in tokens)
            {
                var segment = FormatToken(token);

                stringBuilder.AppendJoin(separator: ' ', segment);
            }

            return stringBuilder.ToString();
        }

        private string FormatToken(Token token)
        {
            var segment = string.Empty;
            if (token.Type == TokenType.Number)
            {
                segment = FormatTicketNumber(token);
            }
            else if (token.Type == TokenType.Code)
            {
                segment = FormatTicketCode(token);
            }

            return segment;
        }

        protected virtual string FormatTicketCode(Token token)
        {
            return token.Value.ToString();
        }

        protected virtual string FormatTicketNumber(Token token)
        {
            return token.Value.ToString();
        }
    }
}
