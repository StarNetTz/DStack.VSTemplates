namespace TemplateDomain.Testing;

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

