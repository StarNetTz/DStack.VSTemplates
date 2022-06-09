using TemplateDomain.ReadModel;
using TemplateDomain.WebApi.ServiceModel;
using ServiceStack;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TemplateDomain.WebApi.ServiceInterface
{
    public class OrganizationQueryService : Service
    {
        readonly IOrganizationQueries Query;
        readonly IQueryById QueryById;

        public OrganizationQueryService(IOrganizationQueries query, IQueryById queryById)
        {
            Query = query;
            QueryById = queryById;
        }

        public async Task<object> Any(FindOrganizations req)
        {
            if (req.Qry.ContainsKey(QueriesKeys.FindByIdKey))
                return await GetById(req);
            else
                return await Query.Execute(req);
        }

        async Task<object> GetById(FindOrganizations req)
        {
            var c = await QueryById.GetById<Organization>(req.Qry[QueriesKeys.FindByIdKey]);
            return c == null ? new PaginatedResult<Organization>() : new PaginatedResult<Organization>() { PageSize = 1, TotalItems = 1, CurrentPage = 0, TotalPages = 1, Data = new List<Organization>() { c } };
        }
    }
}