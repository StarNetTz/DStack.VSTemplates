using TemplateDomain.Domain.Organization;
using TemplateDomain.PL.Commands;
using Moq;
using NServiceBus.Testing;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace TemplateDomain.Domain.NSBus.Tests
{
    public class RegisterOrganizationHandlerTests
    {
        [Fact]
        public async Task ShouldExecute()
        {
            var mock = new Mock<IOrganizationInteractor>();
            mock.Setup(svc => svc.GetPublishedEvents()).Returns(new List<object>());
            var mockObject = mock.Object;
            var handler = new RegisterOrganizationHandler(mockObject);
            var context = new TestableMessageHandlerContext();
            await handler.Handle(new RegisterOrganization(), context).ConfigureAwait(false);
        }
    }
}