using System;
using TemplateDomain.Common;

namespace TemplateDomain.Api.Infrastructure
{
    public class TimeProvider : ITimeProvider
    {
        public DateTime GetUtcTime()
            => DateTime.UtcNow;
    }
}
