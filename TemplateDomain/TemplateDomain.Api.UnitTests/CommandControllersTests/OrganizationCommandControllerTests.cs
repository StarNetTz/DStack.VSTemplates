using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using TemplateDomain.Api.ServiceInterface;
using TemplateDomain.Api.ServiceModel;
using TemplateDomain.Common;
using TemplateDomain.Testing;
using Xunit;

namespace TemplateDomain.Api.UnitTests
{
    public class OrganizationCommandControllerTests
    {
        OrganizationCommandController Controller;

        public OrganizationCommandControllerTests()
        {
            Controller = new OrganizationCommandController(new Mock<IMessageBus>().Object, new Mock<ITimeProvider>().Object, CreateMapper());
            Controller.ControllerContext = CreateTestHttpContext();
        }

        [Fact]
        public async Task Should_Register()
        {
            await Controller.Register(new ServiceModel.RegisterOrganization { Id = "", Name = "", Address = new Address()});
        }

        IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<RegisterOrganization, PL.Commands.RegisterOrganization>());
            return config.CreateMapper();
        }

        static ControllerContext CreateTestHttpContext()
        {
            var hc = new DefaultHttpContext();
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, AuditTestData.DefaultIssuedBy),
                new Claim(ClaimTypes.Role, AuditTestData.AdminRole)
            };
            var claimsIdentity = new ClaimsIdentity(claims);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            hc.User = claimsPrincipal;
            var ctx = new ControllerContext
            {
                HttpContext = hc
            };
            return ctx;
        }
    }
}
