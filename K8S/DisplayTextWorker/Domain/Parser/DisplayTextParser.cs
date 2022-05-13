using System.ComponentModel;
using TicketApi.Contracts.ViewModels;

namespace DisplayTextWorker.Domain.Parser
{
    public class DisplayTextParser
    {
        public IReadOnlyCollection<Token> Parse(TicketParserInputDto input)
        {
            var tokens = new List<Token>();

            if (input.TicketType == TicketType.Regular)
            {
                var codeLength = 4;

                tokens.Add(new Token
                {
                    Type = TokenType.Code,
                    Value = input.DisplayText[..codeLength],
                });
                tokens.Add(new Token
                {
                    Type = TokenType.Number,
                    Value = input.DisplayText[codeLength..],
                });
            }
            else if (input.TicketType == TicketType.Exclusive)
            {
                tokens.Add(new Token
                {
                    Type = TokenType.Number,
                    Value = input.DisplayText,
                });
            }
            else
            {
                throw new InvalidEnumArgumentException($"Wrong enum value for ticket type {input.TicketType}");
            }

            return tokens;
        }
    }
}
