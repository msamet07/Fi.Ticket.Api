using Fi.Ticket.Api.Cqrs;
using Fi.Ticket.Schema.Model;
using FluentValidation;

namespace Fi.Ticket.Api.Impl.Validator
{
    public class CreateTicketPictureValidator : AbstractValidator<CreateTicketPictureCommand>
    {
        public CreateTicketPictureValidator()
        {
            /*
             * If you want to customize at command level, you can use here
             * RuleFor(x => x.Model).SetValidator(new SampleInputModelValidator());
             */
        }
    }

    public class UpdateTicketPictureValidator : AbstractValidator<UpdateTicketPictureCommand>
    {
        public UpdateTicketPictureValidator()
        {
            /*
             * If you want to customize at command level, you can use here
             * RuleFor(x => x.Model).SetValidator(new SampleInputModelValidator());
             */
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}