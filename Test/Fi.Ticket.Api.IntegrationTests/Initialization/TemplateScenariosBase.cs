using Fi.Test.IntegrationTests;

namespace Fi.Ticket.Api.IntegrationTests.Initialization;

public class TicketScenariosBase : FiScenariosBase<TicketApplicationFactory, Startup>
{
    protected TicketScenariosBase(TicketApplicationFactory fiTestApplicationFactory) : base(fiTestApplicationFactory)
    {
    }
}