using System.Threading.Tasks;

namespace TemplateDomain.WebApi.ServiceInterface;

public interface IIdentityProvider
{
    Task<string> GetId(string aggregateName);
}
