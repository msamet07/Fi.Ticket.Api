using Fi.Mediator.Message;
using Fi.Ticket.Schema.Model;
using System.Collections.Generic;

namespace Fi.Ticket.Api.Cqrs
{
    public record CreateTicketPictureCommand(TicketPictureInputModel Model) : CommandBase<TicketPictureOutputModel>;

    public record UpdateTicketPictureCommand(int Id, TicketPictureInputModel Model) : CommandBase<TicketPictureOutputModel>;

    public record DeleteTicketPictureCommand(int Id) : CommandBase<VoidResult>;

    public record GetTicketPictureByKeyQuery(int Id) : QueryBase<TicketPictureOutputModel>;

    public record GetAllTicketPictureQuery : QueryBase<List<TicketPictureOutputModel>>;

}
