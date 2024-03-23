using Fi.Mediator.Message;
using Fi.Ticket.Schema.Model;
using System.Collections.Generic;

namespace Fi.Ticket.Api.Cqrs
{
    public record CreateTicketCommand(TicketInputModel Model) : CommandBase<TicketOutputModel>;

    public record UpdateTicketCommand(int Id, TicketInputModel Model) : CommandBase<TicketOutputModel>;

    public record DeleteTicketCommand(int Id) : CommandBase<VoidResult>;

    public record GetTicketByCodeQuery(string Code) : QueryBase<List<TicketOutputModel>>;

    public record GetTicketByKeyQuery(int Id) : QueryBase<TicketOutputModel>;

    public record GetAllTicketQuery : QueryBase<List<TicketOutputModel>>;
}
//burası benim anladığım kadarıyla bir desingPattern var. Entity i oluşturduktan sonra cqrs altında bunun temel işlemlerini yazmam gerekiyor diye düşünüyoruö
//İsimlendirmede dikkat etmemiz gereken CRUD için Command Baseden gelecek , Select işlemleri için ise sonu Qryden gelecek bunlarda yine altyapıdan geliyor.
//ihtiyaca göre bence bunlar entitynin yapısına göre özelleşebilir.
//gidip Ticket için bir qeryhandler yazmam gerekiyor.