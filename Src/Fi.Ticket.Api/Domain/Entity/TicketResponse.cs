using Fi.Persistence.Relational.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Fi.Infra.Schema.Attributes;

namespace Fi.Ticket.Api.Domain.Entity
{
    [EntityAttribute(EA.ERT.AsIs, EA.EDT.DefinitionData, EA.ECT.Common, EA.ETT.Common, EA.EMT.Mandatory)]
    public class TicketResponse : EntityBaseWithBaseFieldsWithIdentity
    {
        public int TicketId { get; set; }
        public string ResponseText { get; set; }
    }
    public class TicketResponseConfiguration : IEntityTypeConfiguration<TicketResponse>
    {
        public void Configure(EntityTypeBuilder<TicketResponse> builder)
        {
            builder.Property(m => m.TicketId).IsRequired(true);
            builder.Property(m => m.ResponseText).IsRequired(true);
          
        }
    }
}
