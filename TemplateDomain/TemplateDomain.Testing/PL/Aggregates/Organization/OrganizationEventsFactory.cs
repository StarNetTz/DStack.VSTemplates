using TemplateDomain.PL.Events;

namespace TemplateDomain.Testing.PL;

public class OrganizationEventsFactory
{
    public static OrganizationRegistered CreateOrganizationRegisteredEvent(string id)
        => new OrganizationRegistered()
        {
            Id = id,
            IssuedBy = AuditTestData.DefaultIssuedBy,
            TimeIssued = AuditTestData.DefaultTimeIssued,
            Name = "Xamics Ltd",
            Address = AddressTestData.CreateDefault()
        };

    public static OrganizationNameCorrected CreateOrganizationNameCorrectedEvent(string id)
        => new OrganizationNameCorrected()
        {
            Id = id,
            IssuedBy = AuditTestData.DefaultIssuedBy,
            TimeIssued = AuditTestData.DefaultTimeIssued,
            Name = "New name"
        };
}
