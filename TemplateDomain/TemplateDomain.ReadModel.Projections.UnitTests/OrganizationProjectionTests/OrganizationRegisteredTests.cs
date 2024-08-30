namespace TemplateDomain.ReadModel.Projections.Tests;

public class OrganizationRegisteredTests : ProjectionSpecification<OrganizationProjection>
{
    [Fact]
    public async Task Should_Project_OrganizationRegistered()
    {
        var id = $"Organization-{Guid.NewGuid()}";
        await Given(OrganizationEventsFactory.CreateOrganizationRegisteredEvent(id));
        await Expect(OrganizationTestData.CreateDefault(id));
    }
}
