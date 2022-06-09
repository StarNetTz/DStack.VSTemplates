using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TemplateDomain.Api.ServiceModel;
using TemplateDomain.ReadModel;

namespace TemplateDomain.Api.ServiceInterface
{
    [ApiController]
    [Route("lookups")]
    public class LookupsController : ControllerBase
    {
        readonly IQueryById QueryById;

        //   private readonly ILogger<WeatherForecastController> _logger;
        public LookupsController(IQueryById queryById)
        {
            QueryById = queryById;
        }

        [HttpGet]
        public async Task<Lookup> Get(string id)
        {
            return await QueryById.GetById<Lookup>(id);
        }
    }

   
}