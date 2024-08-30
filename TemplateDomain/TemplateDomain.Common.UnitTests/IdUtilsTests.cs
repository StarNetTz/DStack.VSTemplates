using Xunit;

namespace TemplateDomain.Common.UnitTests;

public partial class RecordDictionaryTests
{
    public class IdUtilsTests
    {
        [Theory]
        [InlineData("Teams-1", 1L)]

        public void Should_ConvertToInt64(string inp, long exp)
        {
            Assert.Equal(exp, IdUtils.ToInt64(inp));
        }
    }
}