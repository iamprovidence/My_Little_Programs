using Ardalis.SmartEnum;

namespace TicketApi.Domain
{
    public class TicketType : SmartEnum<TicketType, int>
    {
        public static readonly Regular Regular = new Regular();
        public static readonly Exclusive Exclusive = new Exclusive();

        public TicketType(string name, int value)
            : base(name, value) { }
    }

    public class Regular : TicketType
    {
        public Regular() : base(nameof(Regular), 0) { }
    }

    public class Exclusive : TicketType
    {
        public Exclusive() : base(nameof(Exclusive), 1) { }
    }
}
