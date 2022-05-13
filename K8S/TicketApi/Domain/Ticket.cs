using System.ComponentModel;
using Common.Structs;

namespace TicketApi.Domain
{
    public class Ticket
    {
        public int Id { get; }
        public TicketType Type { get; }
        public string Number { get; }
        public string Code { get; }

        public string DisplayText { get; }
        public MultilingualString FormattedDisplayText { get; }

        [Description("Required for EF")]
        private Ticket() { }

        internal Ticket(
            TicketType type,
            string number,
            string code,
            string displayText)
        {
            if (code.Length != 4) throw new ArgumentException("Code should have length of 4");

            Id = 0;
            Type = type;
            Number = number;
            Code = code;
            DisplayText = displayText;
            FormattedDisplayText = new MultilingualString();
        }

        public void UpdateDisplayText(LanguageCode languageCode, string dislayText)
        {
            FormattedDisplayText[languageCode] = dislayText;
        }
    }
}
