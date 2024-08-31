using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TemplateDomain.Api.UnitTests;

public class OrganizationCommandControllerTests
{
    OrganizationCommandController Controller;

    public OrganizationCommandControllerTests()
    {
        Controller = new OrganizationCommandController(new Mock<IMessageSession>().Object, new MockTimeProvider(), CreateMapper());
        Controller.ControllerContext = CreateTestHttpContext();
    }

    [Fact]
    public async Task Should_Register()
    {
        await Controller.Register(new ServiceModel.Commands.RegisterOrganization { Id = "", Name = "", Address = AddressTestData.CreateDefault()});
    }

    IMapper CreateMapper()
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<ServiceModel.Commands.RegisterOrganization, PL.Commands.RegisterOrganization>());
        return config.CreateMapper();
    }

    static ControllerContext CreateTestHttpContext()
    {
        var hc = new DefaultHttpContext();
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, AuditInfoTestData.DefaultIssuerEmail),
            new Claim(ClaimTypes.Role, AuditInfoTestData.AdminRole)
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
