using Xunit;

namespace TemplateDomain.Common.UnitTests
{
    public class RecordDictionaryTests
    {
        [Fact]
        public void Should_Use_Value_Based_Equality_Test()
        {
            var a = new RecordDictionary<string, Address> { { "a", new Address { City = "London" } } };
            var b = new RecordDictionary<string, Address> { { "a", new Address { City = "London" } } };
            var c = new RecordDictionary<string, Address> { { "c", new Address { City = "London" } } };

            object e = new RecordDictionary<string, Address> { { "a", new Address { City = "London" } } };
            object f = new RecordDictionary<string, Address> { { "a", new Address { City = "London" } } };

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