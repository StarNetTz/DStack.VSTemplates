using Moq;
using TemplateDomain.Api.ServiceInterface;
using TemplateDomain.ReadModel;


namespace TemplateDomain.Api.UnitTests
{
    public class TypeaheadQueryControllerTests
    {
        TypeaheadQueryController Controller;

        public TypeaheadQueryControllerTests()
        {
            Controller = new TypeaheadQueryController(CreateQueryStub());
        }

        [Fact]
        public async Task Should_GetById()
        {
            var res = await Controller.Find(new PaginatedQueryRequest());
            Assert.NotNull(res);
        }

        static ITypeaheadQueries CreateQueryStub()
        {
            var stub = new Mock<ITypeaheadQueries>();
            stub.Setup(x => x.Execute(It.IsAny<PaginatedQueryRequest>())).ReturnsAsync(new PaginatedResult<TypeaheadItem>());
            return stub.Object;
        }
    }
}
