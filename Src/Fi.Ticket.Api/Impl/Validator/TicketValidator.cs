﻿using Fi.Ticket.Api.Cqrs;
using Fi.Ticket.Schema.Model;
using FluentValidation;

namespace Fi.Ticket.Api.Impl.Validator
{
    public class TicketInputModelValidator : AbstractValidator<TicketInputModel>
    {
        public TicketInputModelValidator()
        {
            RuleFor(x => x).NotEmpty();
            RuleFor(x => x.Code).NotEmpty();
            RuleFor(x => x.Code).MaximumLength(10);
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Name).MaximumLength(100);
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Description).MaximumLength(100);
        }
    }
    //Burada dışarıdan gelen veriler için kontroller yazıyoruz.
    public class CreateTicketValidator : AbstractValidator<CreateTicketCommand>
    {
        public CreateTicketValidator()
        {
            /*
             * If you want to customize at command level, you can use here
             * RuleFor(x => x.Model).SetValidator(new SampleInputModelValidator());
             */
        }
    }

    public class UpdateTicketValidator : AbstractValidator<UpdateTicketCommand>
    {
        public UpdateTicketValidator()
        {
            /*
             * If you want to customize at command level, you can use here
             * RuleFor(x => x.Model).SetValidator(new SampleInputModelValidator());
             */
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
