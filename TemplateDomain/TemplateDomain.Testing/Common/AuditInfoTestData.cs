namespace TemplateDomain.Testing;

public static class AuditInfoTestData
{
    public static string DefaultIssuerId => $"{Consts.IdPrefixes.User}7ee41439-faab-411a-a242-c9cf88843d47";
    public static string DefaultIssuerEmail => "johndoe@mail.com";
    public static DateTime DefaultTimeIssued => new DateTime(2008, 5, 5, 7, 15, 21);
    public static string AdminRole => "admin";
    public static string UserName => "admin";
    public static AuditInfo CreateDefault() => new AuditInfo { Time = DefaultTimeIssued, Data = new RecordDictionary<string, string> { { "email", DefaultIssuerEmail } }, Issuer = DefaultIssuerId };
}

