using TemplateDomain.WebApi.ServiceModel;
using System.Threading.Tasks;

namespace TemplateDomain.WebApi.ServiceInterface;

public class OrganizationService : DomainCommandService
{
    public async Task<object> Any(RegisterOrganization request)
        => await TryProcessRequest<PL.Commands.RegisterOrganization>(request);
}