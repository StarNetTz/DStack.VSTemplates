using System;

namespace TemplateDomain.WebApi.ServiceInterface
{
    public interface ITimeProvider
    {
        DateTime GetUtcTime();
    }
}
