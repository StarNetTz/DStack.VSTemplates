using System;
using System.Linq;
using TemplateDomain.Common;
using TemplateDomain.PL.Commands;

namespace TemplateDomain.Testing.PL
{
    public class OrganizationCommandsFactory
    {
        public static RegisterOrganization CreateRegisterOrganizationCommand(string id)
            => new RegisterOrganization()
            {
                Id = id,
                IssuedBy = "zeko",
                Name = "Xamics Ltd",
                TimeIssued = DateTime.MinValue,
                Address = CommonTestData.CreateAddress()
            };

        public static CorrectOrganizationName CreateCorrectOrganizationNameCommand(string id)
            => new CorrectOrganizationName()
            {
                Id = id,
                IssuedBy = "zeko",
                TimeIssued = DateTime.MinValue,
                Name = "New name"
            };
    }
}
