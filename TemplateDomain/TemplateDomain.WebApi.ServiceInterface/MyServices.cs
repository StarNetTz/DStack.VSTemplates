using System;
using ServiceStack;
using TemplateDomain.WebApi.ServiceModel;

namespace TemplateDomain.WebApi.ServiceInterface
{
    public class MyServices : Service
    {
        public object Any(Hello request)
        {
            return new HelloResponse { Result = $"Hello, {request.Name}!" };
        }
    }
}
