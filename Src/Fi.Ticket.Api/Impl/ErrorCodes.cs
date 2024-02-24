using Fi.Infra.Exceptions;

namespace Fi.Ticket.Api.Impl
{
    public class ErrorCodes : BaseErrorCodes
    {
        public static FiBusinessReason SampleAlreadyExists => new FiBusinessReason(1,
            "A sample with same Code already exists. {0} ({1}).");
        public static FiBusinessReason TicketAlreadyExists => new FiBusinessReason(2,
           "A ticket with same Code already exists. {0} ({1}).");
        public static FiBusinessReason TicketPictureAlreadyExists => new FiBusinessReason(3,
           "A TicketPicture with same id already exists. {0} ({1}).");
        public static FiBusinessReason TicketResponseAlreadyExists => new FiBusinessReason(4,
           "A TicketResponse with same id already exists. {0} ({1}).");

    }
}
