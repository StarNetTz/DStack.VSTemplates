using TemplateDomain.Testing;
using Xunit;

namespace TemplateDomain.Common.UnitTests
{
    public partial class RecordDictionaryTests
    {
        [Fact]
        public void Should_Use_Value_Based_Equality_Test()
        {
            var a = new RecordDictionary<string, Address> { { "a", CommonTestData.CreateAddress() } };
            var b = new RecordDictionary<string, Address> { { "a", CommonTestData.CreateAddress() } };
            var c = new RecordDictionary<string, Address> { { "c", CommonTestData.CreateAddress() } };

            object e = new RecordDictionary<string, Address> { { "a", CommonTestData.CreateAddress() } };
            object f = new RecordDictionary<string, Address> { { "a", CommonTestData.CreateAddress() } };

            RecordDictionary<string, Address> d = null;

            Assert.True(a.Equals(b));
            Assert.True(a.GetHashCode() == b.GetHashCode());
            Assert.True(a == b);
            Assert.True(a != c);
            Assert.True(a != d);
            Assert.True(Equals(e, f));
        }
    }
}