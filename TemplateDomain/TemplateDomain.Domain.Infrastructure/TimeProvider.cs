using System;
using TemplateDomain.Common;

namespace TemplateDomain.Domain.Infrastructure
{
    public class TimeProvider : ITimeProvider
    {
        public DateTime GetUtcTime()
            => DateTime.UtcNow;
    }
}
