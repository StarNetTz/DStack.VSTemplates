﻿using TemplateDomain.Common;
using TemplateDomain.PL.Events;
using System;

namespace TemplateDomain.Domain.UnitTests.OrganizationTests
{
    public class EventsFactory
    {
        public static OrganizationRegistered CreateOrganizationRegisteredEvent(string id)
            => new OrganizationRegistered()
            {
                Id = id,
                IssuedBy = "zeko",
                Name = "Xamics Ltd",
                TimeIssued = DateTime.MinValue,
                Address = new Address
                {
                    Street = "321 Bakers Street b",
                    City = "London",
                    Country = "UK",
                    State = "Essex",
                    PostalCode = "3021"
                }
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