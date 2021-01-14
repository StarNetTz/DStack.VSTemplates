using ServiceStack;
using $ext_projectname$.WebApi.ServiceInterface;
using $ext_projectname$.WebApi.ServiceModel;
using System.Threading.Tasks;
using Xunit;
using $ext_projectname$.Common;

namespace $safeprojectname$
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
                    Country = "UK",
                    State = "Essex",
                    Street = "Baker 223",
                    PostalCode = "9876"
                }
            }) as ResponseStatus;

            Assert.Null(response.Errors);
        }
    }
}