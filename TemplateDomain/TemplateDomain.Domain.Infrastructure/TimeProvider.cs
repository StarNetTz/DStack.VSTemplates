using System;

namespace TemplateDomain.Domain.Infrastructure
{
    public class TimeProvider : ITimeProvider
    {
        public DateTime GetUtcTime()
            => DateTime.UtcNow;
    }
}
