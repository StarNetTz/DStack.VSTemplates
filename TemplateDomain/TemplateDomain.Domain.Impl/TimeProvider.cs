using System;
using TemplateDomain.Common;

namespace TemplateDomain.Domain.Impl
{
    public class TimeProvider : ITimeProvider
    {
        public DateTime GetUtcTime()
            => DateTime.UtcNow;
    }
}
