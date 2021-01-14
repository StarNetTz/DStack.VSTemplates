using $ext_projectname$.ReadModel;
using $ext_projectname$.WebApi.ServiceModel;
using ServiceStack;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace $safeprojectname$
{
    public class OrganizationQueryService : Service
    {
        readonly IOrganizationSearchQuery Query;
        readonly IQueryById QueryById;

        public OrganizationQueryService(IOrganizationSearchQuery query, IQueryById queryById)
        {
            Query = query;
            QueryById = queryById;
        }

        public async Task<object> Any(FindOrganizations req)
        {
            if (!string.IsNullOrEmpty(req.Id))
                return await GetById(req);
            return await Search(req);
        }

        async Task<object> GetById(FindOrganizations req)
        {
            var c = await QueryById.GetById<Organization>(req.Id);
            return c == null ? new PaginatedResult<Organization>() : new PaginatedResult<Organization>() { PageSize = 1, TotalItems = 1, CurrentPage = 0, TotalPages = 1, Data = new List<Organization>() { c } };
        }

        async Task<object> Search(FindOrganizations req)
        {
            var SearchRequest = req.ConvertTo<SearchQueryRequest>();
            var res = await Query.Execute(SearchRequest);
            return res;
        }
    }
}