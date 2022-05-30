using TemplateDomain.Common;

namespace TemplateDomain.Testing
{
    public static class CommonTestData
    {
        public static Address CreateAddress() => new Address
        {
            Street = "321 Bakers Street b",
            City = "London",
            Country = "UK",
            State = "Essex",
            PostalCode = "3021"
        };
    }
}
