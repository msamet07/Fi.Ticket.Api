using Fi.Ticket.Api.Cqrs;
using Fi.Ticket.Schema.Model;
using FluentValidation;

namespace Fi.Ticket.Api.Impl.Validator
{
    public class TicketResponseInputModelValidator : AbstractValidator<TicketResponseInputModel>
    {
        public TicketResponseInputModelValidator()
        {
            RuleFor(x => x).NotEmpty();
            RuleFor(x => x.TicketId).NotEmpty();
            RuleFor(x => x.ResponseText).NotEmpty();
            RuleFor(x => x.ResponseText).MaximumLength(500);
        }
    }

    public class CreateTicketResponseValidator : AbstractValidator<CreateTicketResponseCommand>
    {
        public CreateTicketResponseValidator()
        {
            /*
             * If you want to customize at command level, you can use here
             * RuleFor(x => x.Model).SetValidator(new SampleInputModelValidator());
             */
        }
    }

    public class UpdateTicketResponseValidator : AbstractValidator<UpdateTicketResponseCommand>
    {
        public UpdateTicketResponseValidator()
        {
            /*
             * If you want to customize at command level, you can use here
             * RuleFor(x => x.Model).SetValidator(new SampleInputModelValidator());
             */
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}