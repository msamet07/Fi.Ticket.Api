using Fi.Infra.Schema.Attributes;
using Fi.Infra.Schema.Utility;

namespace Fi.Ticket.Schema.Model
{
    public class SampleType : FiSmartEnum<SampleType, byte>
    {
        public static readonly SampleType Specific = new("Specific", 1);
        public static readonly SampleType Common = new("Common", 2);

        public SampleType(string code, byte value, string name = null) : base(code, value, name)
        {
        }
    }
    public class TicketStatus : FiSmartEnum<TicketStatus, byte>
    {
        public static readonly TicketStatus Answered = new("Answered", 1);
        public static readonly TicketStatus Unanswered = new("Unanswered", 2);
        public static readonly TicketStatus Unsolved = new("Unsolved", 3);

        public TicketStatus(string code, byte value, string name = null) : base(code, value, name)
        {
        }
    }
}
