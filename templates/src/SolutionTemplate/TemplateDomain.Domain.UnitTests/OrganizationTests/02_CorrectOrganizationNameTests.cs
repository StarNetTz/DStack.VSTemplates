using System.Threading.Tasks;
using Xunit;

namespace $safeprojectname$.OrganizationTests
{
    public class CorrectOrganizationNameTests : OrganizationAggregateTester
    {
        [Fact]
        public async Task Should_Execute_Command()
        {
            var aggId = "Organizations-1";
            Given(EventsFactory.CreateOrganizationRegisteredEvent(aggId));
            When(CommandsFactory.CreateCorrectOrganizationNameCommand(aggId));
            await Expect(EventsFactory.CreateOrganizationNameCorrectedEvent(aggId));
        }

        [Fact]
        public async Task Should_Throw_DomainError_On_No_Organization()
        {
            var aggId = "Organizations-1";
            Given();
            When(CommandsFactory.CreateCorrectOrganizationNameCommand(aggId));
            await ExpectError("OrganizationDoesNotExist");
        }

        [Fact]
        public async Task Command_Is_Idempotent()
        {
            var aggregateId = "Organizations-1";
            Given(
                EventsFactory.CreateOrganizationRegisteredEvent(aggregateId),
                EventsFactory.CreateOrganizationNameCorrectedEvent(aggregateId));

            When(CommandsFactory.CreateCorrectOrganizationNameCommand(aggregateId));

            await ExpectNoEvents();
        }
    }
}