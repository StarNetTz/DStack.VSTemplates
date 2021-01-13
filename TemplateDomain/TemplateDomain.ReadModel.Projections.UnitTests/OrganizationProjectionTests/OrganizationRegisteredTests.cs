using TemplateDomain.Common;
using TemplateDomain.PL.Events;
using DStack.Projections.Testing;
using System;
using System.Threading.Tasks;
using Xunit;

namespace TemplateDomain.ReadModel.Projections.Tests
{
    public class OrganizationRegisteredTests : ProjectionSpecification<OrganizationProjection>
    {
        [Fact]
        public async Task Should_Project_OrganizationRegistered()
        {
            var id = $"Organization-{Guid.NewGuid()}";
            await Given(new OrganizationRegistered() { Id = id, Name = "Betting Shop Royal", Address = new Address { Street = "My street", City = "My City", Country = "My Country", State = "TK", PostalCode = "75000" } });
            await Expect(new Organization() { Id = id, Name = "Betting Shop Royal", Address = new Address { Street = "My street", City = "My City", Country = "My Country", State = "TK", PostalCode = "75000" } });
        }
    }
}
