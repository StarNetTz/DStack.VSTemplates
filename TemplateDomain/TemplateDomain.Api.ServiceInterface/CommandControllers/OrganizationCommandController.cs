using TemplateDomain.Api.ServiceModel.Commands;

namespace TemplateDomain.Api.ServiceInterface;


[Route("commands/organization")]
[ApiController]
//[Authorize]
public class OrganizationCommandController : CommandControllerBase
{
    public OrganizationCommandController(IMessageSession bus, ITimeProvider timeProvider, IMapper mapper) : base(bus, timeProvider, mapper)
    {
    }

    [HttpPost("register")]
    public async Task Register([FromBody]RegisterOrganization cmd)
    {
        await MapAndProcessRequest<PL.Commands.RegisterOrganization>(cmd);
    }
}
