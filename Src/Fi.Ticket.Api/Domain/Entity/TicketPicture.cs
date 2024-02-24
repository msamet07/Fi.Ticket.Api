using Fi.Persistence.Relational.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Fi.Infra.Schema.Attributes;

namespace Fi.Ticket.Api.Domain.Entity
{

    [EntityAttribute(EA.ERT.AsIs, EA.EDT.DefinitionData, EA.ECT.Common, EA.ETT.Common, EA.EMT.Mandatory)]
    public class TicketPicture : EntityBaseWithBaseFieldsWithIdentity
    {
        public int TicketId { get; set; }
        public int PictureId { get; set; }

    }
    public class TicketPictureConfiguration : IEntityTypeConfiguration<TicketPicture>
    {
        public void Configure(EntityTypeBuilder<TicketPicture> builder)
        {
            builder.Property(m => m.TicketId).IsRequired(true);
            builder.Property(m => m.PictureId).IsRequired(true);
            builder.HasIndex(p => new { p.PictureId }).IsUnique();
        }
    }
}
