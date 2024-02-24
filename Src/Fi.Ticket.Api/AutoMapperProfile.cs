using AutoMapper;
using Fi.Ticket.Api.Domain.Entity;

//using Fi.Ticket.Api.Domain;
//using Fi.Ticket.Api.Domain.Entity;
using Fi.Ticket.Schema.Model;

namespace Fi.Ticket.Api
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<SampleInputModel,Sample>();
            CreateMap<Sample, SampleOutputModel>();
            CreateMap<Fi.Ticket.Api.Domain.Entity.Ticket, TicketOutputModel>();
            CreateMap<TicketInputModel, Fi.Ticket.Api.Domain.Entity.Ticket>();
            CreateMap<Fi.Ticket.Api.Domain.Entity.TicketPicture, TicketPictureOutputModel>();
            CreateMap<Fi.Ticket.Api.Domain.Entity.TicketResponse, TicketResponseOutputModel>();
            CreateMap<TicketResponseInputModel, Fi.Ticket.Api.Domain.Entity.TicketResponse>();

        }
    }
}