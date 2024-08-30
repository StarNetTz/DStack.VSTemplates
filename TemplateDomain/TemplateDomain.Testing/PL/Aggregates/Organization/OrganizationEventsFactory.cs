using TemplateDomain.PL.Events;

namespace TemplateDomain.Testing.PL;

public class OrganizationEventsFactory
{
    public static OrganizationRegistered CreateOrganizationRegisteredEvent(string id)
        => new OrganizationRegistered()
        {
            Id = id,
            AuditInfo = AuditInfoTestData.CreateDefault(),
            Name = "Xamics Ltd",
            Address = AddressTestData.CreateDefault()
        };

    public static OrganizationNameCorrected CreateOrganizationNameCorrectedEvent(string id)
        => new OrganizationNameCorrected()
        {
            Id = id,
            AuditInfo = AuditInfoTestData.CreateDefault(),
            Name = "New name"
        };
}
