using TemplateDomain.PL.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateDomain.PL.Commands;
using TemplateDomain.Common;

namespace TemplateDomain.Testing.PL
{
    public class OrganizationEventsFactory
    {
        public static OrganizationRegistered CreateOrganizationRegisteredEvent(string id)
            => new OrganizationRegistered()
            {
                Id = id,
                IssuedBy = "zeko",
                Name = "Xamics Ltd",
                TimeIssued = DateTime.MinValue,
                Address = CommonTestData.CreateAddress()
            };

        public static OrganizationNameCorrected CreateOrganizationNameCorrectedEvent(string id)
            => new OrganizationNameCorrected()
            {
                Id = id,
                IssuedBy = "zeko",
                TimeIssued = DateTime.MinValue,
                Name = "New name"
            };
    }

}
