using System;

namespace TemplateDomain.Domain
{
    public interface ITimeProvider
    {
        DateTime GetUtcTime();
    }
}
