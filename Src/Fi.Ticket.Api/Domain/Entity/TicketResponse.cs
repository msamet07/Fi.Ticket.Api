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
    public class TicketResponseConfiguration : IEntityTypeConfiguration<TicketResponse>//konfigürasyon ayarlarım için TicketResponseConfigration clasıı açtım ve IEntityTypeConfiguration dan türettim buda yine Altyapıdan geliyor.
        //Bu interface diyorki bana bana istediğin configüre ayarlarını ver ben veritabanında bunu configüre edicem.
    {
        public void Configure(EntityTypeBuilder<TicketResponse> builder)
        {
            builder.Property(m => m.TicketId).IsRequired(true);//bu 2 alanda dolmak zorunda şeklinde konfigüre ettim.
            builder.Property(m => m.ResponseText).IsRequired(true);
          
        }
    }
}
//ayarları yaptım , ama yeterli değil devamında migration oluşturmam gerekiyor.
//dotnet ef migrations add xxx komutuyla oluşturuyorum.
//ardından updateDatabese demem gerekiyor,aradaki tüm farklılıkları yok ediyorum.
