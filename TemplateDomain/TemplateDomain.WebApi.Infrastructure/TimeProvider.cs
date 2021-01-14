using System;
using TemplateDomain.Common;

namespace TemplateDomain.WebApi.Infrastructure
{
    public class TimeProvider : ITimeProvider
    {
        public DateTime GetUtcTime()
            => DateTime.UtcNow;
    }
}
