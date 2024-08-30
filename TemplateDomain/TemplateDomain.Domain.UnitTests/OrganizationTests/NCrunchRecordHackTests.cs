using Xunit;

namespace TemplateDomain.Domain.UnitTests.OrganizationTests;

public class NCrunchRecordHackTests
{
    [Fact]
    public void Should_invoke_cloning_constructor()
    {
        Starnet.Common.Utils.RecordUtilsForNCrunch.InvokeSecondaryConstructorOfRecordsInTheAssemblyTypes(
            typeof(PL.Commands.RegisterOrganization));
    }
}
