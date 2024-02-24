using Fi.Infra.Schema.Json;
using Fi.Infra.Schema.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fi.Ticket.Schema.Model
{
    public record TicketInputModel : InputModelBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public short Age { get; set; }
        public string IdNumber { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Code { get; set; }
        public List<byte[]> Pictures { get; set; }

        [JsonConverter(typeof(FiSmartEnumCodeConverter<TicketStatus, byte>))]
        public TicketStatus Status { get; set; }
    }

    public record TicketOutputModel : OutputModelBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public short Age { get; set; }
        public string IdNumber { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Code { get; set; }

        [JsonConverter(typeof(FiSmartEnumCodeConverter<TicketStatus, byte>))]
        public TicketStatus Status { get; set; }
    }
}
