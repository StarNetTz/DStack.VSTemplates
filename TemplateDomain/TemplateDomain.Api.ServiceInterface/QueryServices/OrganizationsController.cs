using Microsoft.AspNetCore.Mvc;
using TemplateDomain.ReadModel;

namespace TemplateDomain.Api.ServiceInterface
{
    [ApiController]
    [Route("organizations")]
    public class OrganizationsController : ControllerBase
    {
        readonly IOrganizationQueries Query;
        readonly IQueryById QueryById;

        public OrganizationsController(IOrganizationQueries query, IQueryById queryById)
        {
            Query = query;
            QueryById = queryById;
        }

        [HttpPost]
        public async Task<PaginatedResult<Organization>> Find(PaginatedQueryRequest req)
        {
            if (req.Qry.ContainsKey(QueriesKeys.FindByIdKey))
                return await GetById(req);
            else
                return await Query.Execute(req);
        }

        async Task<PaginatedResult<Organization>> GetById(PaginatedQueryRequest req)
        {
            var c = await QueryById.GetById<Organization>(req.Qry[QueriesKeys.FindByIdKey]);
            return c == null ?
                new PaginatedResult<Organization>() :
                new PaginatedResult<Organization>() {
                    PageSize = 1,
                    TotalItems = 1,
                    CurrentPage = 0,
                    TotalPages = 1,
                    Data = new List<Organization>() { c }
                };
        }
    }
}