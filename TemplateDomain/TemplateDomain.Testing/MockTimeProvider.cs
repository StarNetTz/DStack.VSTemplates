using TemplateDomain.Common;

namespace TemplateDomain.Testing
{
    public class MockTimeProvider : ITimeProvider
    {
        public DateTime Time { get; set; } = DateTime.MinValue;
        public DateTime GetUtcTime() => Time;
    }
}
