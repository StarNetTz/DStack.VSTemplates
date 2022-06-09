using System.Threading.Tasks;
using TemplateDomain.Testing.PL;
using Xunit;

namespace TemplateDomain.Domain.UnitTests.OrganizationTests
{
    public class CorrectOrganizationNameTests : OrganizationAggregateTester
    {
        [Fact]
        public async Task Should_Execute_Command()
        {
            var aggId = "Organizations-1";
            Given(OrganizationEventsFactory.CreateOrganizationRegisteredEvent(aggId));
            When(OrganizationCommandsFactory.CreateCorrectOrganizationNameCommand(aggId));
            await Expect(OrganizationEventsFactory.CreateOrganizationNameCorrectedEvent(aggId));
        }

        [Fact]
        public async Task Should_Throw_DomainError_On_No_Organization()
        {
            var aggId = "Organizations-1";
            Given();
            When(OrganizationCommandsFactory.CreateCorrectOrganizationNameCommand(aggId));
            await ExpectError("OrganizationDoesNotExist");
        }

        [Fact]
        public async Task Command_Is_Idempotent()
        {
            var aggregateId = "Organizations-1";
            Given(
                OrganizationEventsFactory.CreateOrganizationRegisteredEvent(aggregateId),
                OrganizationEventsFactory.CreateOrganizationNameCorrectedEvent(aggregateId));

            When(OrganizationCommandsFactory.CreateCorrectOrganizationNameCommand(aggregateId));

            await ExpectNoEvents();
        }
    }
}