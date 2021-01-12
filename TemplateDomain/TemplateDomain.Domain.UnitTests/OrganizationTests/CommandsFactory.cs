﻿using TemplateDomain.PL.Commands;
using System;

namespace TemplateDomain.Domain.UnitTests.OrganizationTests
{
    public class CommandsFactory
    {
        public static RegisterOrganization CreateRegisterOrganizationCommand(string id)
            => new RegisterOrganization()
            {
                Id = id,
                IssuedBy = "zeko",
                Name = "Xamics Ltd",
                TimeIssued = DateTime.MinValue,
                Address = new PL.Address
                {
                    Street = "321 Bakers Street b",
                    City = "London",
                    Country = "UK",
                    State = "Essex",
                    PostalCode = "3021"
                }
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