using System;

namespace TemplateDomain.Common
{
    public interface ITimeProvider
    {
        DateTime GetUtcTime();
    }
}
