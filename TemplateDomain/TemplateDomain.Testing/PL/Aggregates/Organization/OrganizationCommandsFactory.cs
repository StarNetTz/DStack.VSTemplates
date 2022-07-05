using TemplateDomain.PL.Commands;

namespace TemplateDomain.Testing.PL
{
    public class OrganizationCommandsFactory
    {
        public static RegisterOrganization CreateRegisterOrganizationCommand(string id)
            => new RegisterOrganization()
            {
                Id = id,
                IssuedBy = AuditTestData.DefaultIssuedBy,
                TimeIssued = AuditTestData.DefaultTimeIssued,
                Name = "Xamics Ltd",
                Address = AddressTestData.CreateDefault()
            };

        public static CorrectOrganizationName CreateCorrectOrganizationNameCommand(string id)
            => new CorrectOrganizationName()
            {
                Id = id,
                IssuedBy = AuditTestData.DefaultIssuedBy,
                TimeIssued = AuditTestData.DefaultTimeIssued,
                Name = "New name"
            };
    }
}
