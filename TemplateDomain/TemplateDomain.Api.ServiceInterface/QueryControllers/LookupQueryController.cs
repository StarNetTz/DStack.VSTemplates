using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TemplateDomain.ReadModel;

namespace TemplateDomain.Api.ServiceInterface;

[ApiController]
[Route("lookups")]
public class LookupQueryController : ControllerBase
{
    readonly IQueryById QueryById;

    public LookupQueryController(IQueryById queryById)
    {
        QueryById = queryById;
    }
    
    [HttpGet]
    public async Task<Lookup> Get(string id)
    {
        return await QueryById.GetById<Lookup>(id);
    }
}