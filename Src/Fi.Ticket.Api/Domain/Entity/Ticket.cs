using Fi.Persistence.Relational.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Fi.Ticket.Schema.Model;
using Fi.Infra.Schema.Attributes;

namespace Fi.Ticket.Api.Domain.Entity
{
    //Aslında Entitiler bizim veri tabanımızdaki tablolara tekabül ediyor.Benim burada açtığım her sınıf EntityBaseWithBaseFieldsWithIdentity  
    // den türediği sürece bunu bir tablo olarak algılıyor.ID vs zaten baseden geliyor.

    [EntityAttribute(EA.ERT.AsIs, EA.EDT.DefinitionData, EA.ECT.Common, EA.ETT.Common, EA.EMT.Mandatory)]
    public class Ticket : EntityBaseWithBaseFieldsWithIdentity 
    {
        public string Name { get; set; }//Pdf de yazan kullanıcıdan istenilen bilgiler.
        public string Surname { get; set; }
        public short Age { get; set; }
        public string IdNumber { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Code { get; set; }//her ticket a bir kod atayacağım için bir kod alanı oluşturuyorum.
        public TicketStatus Status { get; set; }
    }
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.Property(m => m.Code).IsRequired(true).HasMaxLength(10);
            builder.Property(m => m.Name).IsRequired(true).HasMaxLength(100);
            builder.Property(m => m.Description).IsRequired(true).HasMaxLength(500);

            builder.Property(m => m.Status).IsRequired(true)
                   .HasConversion(
                       p => p.Value,
                       p => TicketStatus.FromValue(p)
                   );

            builder.HasIndex(p => new { p.Code }).IsUnique();
        }
    }
}
