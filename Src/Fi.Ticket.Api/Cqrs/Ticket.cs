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
