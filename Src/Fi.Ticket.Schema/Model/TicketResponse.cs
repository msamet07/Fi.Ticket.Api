using Fi.Infra.Schema.Json;
using Fi.Infra.Schema.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fi.Ticket.Schema.Model
{
    public record TicketResponseInputModel : InputModelBase
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string ResponseText { get; set; }
    }

    public record TicketResponseOutputModel : OutputModelBase
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string ResponseText { get; set; }
    }
}
