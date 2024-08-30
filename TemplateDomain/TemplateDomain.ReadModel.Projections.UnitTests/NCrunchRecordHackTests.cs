using Xunit;

namespace TemplateDomain.ReadModel.Projections.Tests;

public class NCrunchRecordHackTests
{
    [Fact]
    public void Should_invoke_cloning_constructor()
    {
        Starnet.Common.Utils.RecordUtilsForNCrunch.InvokeSecondaryConstructorOfRecordsInTheAssemblyTypes(
            typeof(Lookup));
    }
}
