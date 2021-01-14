using System.Threading.Tasks;
using Xunit;

namespace $safeprojectname$.OrganizationTests
{
    public class RegisterOrganizationTests : OrganizationAggregateTester
    {

        [Fact]
        public async Task Should_Execute_Command()
        {
            string aggId = "Organizations-1";
            Given();
            When(CommandsFactory.CreateRegisterOrganizationCommand(aggId));
            await Expect(EventsFactory.CreateOrganizationRegisteredEvent(aggId));
        }

        [Fact]
        public async Task Command_Is_Idempotent()
        {
            string aggId = "Organizations-1";
            var cmd = CommandsFactory.CreateRegisterOrganizationCommand(aggId);
            var evt = EventsFactory.CreateOrganizationRegisteredEvent(aggId);

            Given(evt);
            When(cmd);
            await ExpectNoEvents();
        }

        [Fact]
        public async Task NonIdempotent_Command__Should_Throw_DomainError()
        {
            string aggId = "Organizations-1";
            var cmd = CommandsFactory.CreateRegisterOrganizationCommand(aggId);
            cmd.Address.Country = "Some other";
            var evt = EventsFactory.CreateOrganizationRegisteredEvent(aggId);

            Given(evt);
            When(cmd);
            await ExpectError("OrganizationAlreadyExists");
        }
    }
}