using Fi.Infra.Schema.Json;
using Fi.Infra.Schema.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fi.Ticket.Schema.Model
{
    public record TicketPictureInputModel : InputModelBase
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public int PictureId { get; set; }
    }


    public record TicketPictureOutputModel : OutputModelBase
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public int PictureId { get; set; }
        public string DataBase64 { get; set; }
    }
}
