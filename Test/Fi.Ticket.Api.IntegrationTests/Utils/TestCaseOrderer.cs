using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Fi.Ticket.Api.IntegrationTests
{
    public class TicketTestCaseOrderer : ITestCaseOrderer
    {
        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
        {
            return testCases.OrderBy(tc => tc.DisplayName);
        }
    }
}