using ServiceStack;
using TemplateDomain.WebApi.ServiceInterface;
using TemplateDomain.WebApi.ServiceModel;
using System.Threading.Tasks;
using Xunit;
using TemplateDomain.Common;

namespace TemplateDomain.WebApi.UnitTests
{
    public class OrganizationCommandServiceTests : IClassFixture<CommandServiceTestBase<OrganizationService>>
    {
        OrganizationService Service;

        public OrganizationCommandServiceTests(CommandServiceTestBase<OrganizationService> fixture)
        {
            Service = fixture.Service;
        }

        [Fact]
        public async Task can_execute_register_company()
        {
            var response = await Service.Any(new RegisterOrganization
            {
                Id = "Organizations-1",
                Name = "My company",
                Address = new Address
                {
                    City = "Cardiff",
                    Country = new Ref { Id = "EN", Val = "England" },
                    State = "Essex",
                    Street = "Baker 223",
                    PostalCode = "9876"
                }
            }) as ResponseStatus;

            Assert.Null(response.Errors);
        }
    }
}