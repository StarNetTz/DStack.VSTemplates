using Moq;
using TemplateDomain.ReadModel;

namespace TemplateDomain.Api
{
    public static class MockFactory
    {
        public static IQueryById CreateQueryByIdMock()
        {
            var queryByIdMock = new Mock<IQueryById>();
            queryByIdMock.Setup(x => x.GetById<Lookup>(It.IsAny<string>())).ReturnsAsync(new Lookup
            {
                Id = "Lookups-Countries",
                Data = new System.Collections.Generic.List<LookupItem> {
                    new LookupItem { Id = "1", Value = "USA" }
                }
            });
            return queryByIdMock.Object;
        }

        public static IOrganizationQueries CreateOrganizationQueriesMock()
        {
            var queryByIdMock = new Mock<IOrganizationQueries>();
            return queryByIdMock.Object;
        }
    }
}
