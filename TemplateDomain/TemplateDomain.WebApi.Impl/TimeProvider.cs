using System;
using TemplateDomain.Common;

namespace TemplateDomain.WebApi.Impl
{
    public class TimeProvider : ITimeProvider
    {
        public DateTime GetUtcTime()
            => DateTime.UtcNow;
    }
}
