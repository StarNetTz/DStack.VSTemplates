using System;
using TemplateDomain.Common;

namespace TemplateDomain.Api.Impl
{
    public class TimeProvider : ITimeProvider
    {
        public DateTime GetUtcTime()
            => DateTime.UtcNow;
    }
}
