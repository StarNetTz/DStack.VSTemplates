using TemplateDomain.ReadModel;
using TemplateDomain.WebApi.ServiceModel;
using ServiceStack;
using System.Threading.Tasks;

namespace TemplateDomain.WebApi.ServiceInterface
{
    public class LookupService : Service
    {
        readonly IQueryById QueryById;

        public LookupService(IQueryById queryById)
        {
            QueryById = queryById;
        }

        public async Task<object> Any(GetLookup req)
            => await QueryById.GetById<Lookup>(req.Id);
    }
}