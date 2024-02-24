using Fi.Mediator.Message;
using Fi.Ticket.Schema.Model;
using System.Collections.Generic;

namespace Fi.Ticket.Api.Cqrs
{
    public record CreateTicketResponseCommand(TicketResponseInputModel Model) : CommandBase<TicketResponseOutputModel>;

    public record UpdateTicketResponseCommand(int Id, TicketResponseInputModel Model) : CommandBase<TicketResponseOutputModel>;

    public record DeleteTicketResponseCommand(int Id) : CommandBase<VoidResult>;

    public record GetTicketResponseByKeyQuery(int Id) : QueryBase<TicketResponseOutputModel>;

    public record GetAllTicketResponseQuery : QueryBase<List<TicketResponseOutputModel>>;

}
