using Fi.Ticket.Api.Domain.Entity;
using Fi.Ticket.Api.IntegrationTests.Initialization;
using Fi.Ticket.Api.Persistence;
using Fi.Ticket.Schema.Model;
using Fi.Infra.Exceptions;
using Fi.Infra.Schema.Const;
using Fi.Infra.Schema.Model;
using Fi.Mediator.Message;
using Fi.Persistence.Relational.Interfaces;
using Fi.Test.Extensions;
using Fi.Test.IntegrationTests.IntegrationTestHelper;
using FizzWare.NBuilder;
using Microsoft.EntityFrameworkCore;
using Should;
using Xunit;

namespace Fi.Ticket.Api.IntegrationTests.Controllers;

//[TestCaseOrderer("Fi.Ticket.Api.IntegrationTests.TicketTestCaseOrderer", "Fi.Ticket.Api.IntegrationTests")]
public class SamplesControllerTests : TicketScenariosBase
{
    private const string basePath = "api/v1/Ticket/Samples";

    public SamplesControllerTests(TicketApplicationFactory fiTestApplicationFactory) : base(fiTestApplicationFactory)
    {
    }
}