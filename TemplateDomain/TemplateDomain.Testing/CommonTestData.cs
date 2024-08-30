using TemplateDomain.Common;

namespace TemplateDomain.Testing
{
    public static class AddressTestData
    {
        public static Address CreateDefault() => new Address
        {
            Street = "321 Bakers Street b",
            City = "London",
            Country = new Ref { Id = "EN", Val = "England" },
            State = "Essex",
            PostalCode = "3021"
        };
    }

    public static class AuditTestData
    {
        public static string DefaultIssuedBy  => "johndoe@mail.com";
        public static string AdminRole => "adminjohndoe@mail.com";

        public static DateTime DefaultTimeIssued => new DateTime(2008, 5, 5, 7, 15, 21);
    }
}
