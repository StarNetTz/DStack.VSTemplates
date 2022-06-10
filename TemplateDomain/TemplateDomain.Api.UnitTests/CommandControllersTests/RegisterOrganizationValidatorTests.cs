using TemplateDomain.Api.ServiceInterface;
using TemplateDomain.Api.ServiceModel;
using Xunit;

namespace TemplateDomain.Api.UnitTests
{
    public class RegisterOrganizationValidatorTests : ValidatorTestBase<RegisterOrganization>
    {
        public RegisterOrganizationValidatorTests() : base(new RegisterOrganizationValidator()) { }

        [Theory]
        [InlineData("Id", "NotEmptyValidator")]
        [InlineData("Name", "NotEmptyValidator")]
        [InlineData("Address", "NotEmptyValidator")]
        public async Task Property_Is_Required(string property, string errorCode)
        {
            var obj = new RegisterOrganization();
            await AssertRuleBroken(obj, property, errorCode);
        }

        [Theory]
        [InlineData(1, 151)]
        public async Task Name_Length_Should_Be_In_Closed_Range_2_150(int belowMin, int aboveMax)
        {
            await AssertNameLengthRuleBroken(belowMin);
            await AssertNameLengthRuleBroken(aboveMax);
        }

            async Task AssertNameLengthRuleBroken(int len)
            {
                var obj = new RegisterOrganization { Name = CreateStringOfLength(len) };
                await AssertRuleBroken(obj, "Name", "LengthValidator");
            }
    }
}