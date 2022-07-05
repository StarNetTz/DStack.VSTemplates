using TemplateDomain.WebApi.ServiceModel;
using ServiceStack.FluentValidation;

namespace TemplateDomain.WebApi.ServiceInterface
{
    public class RegisterOrganizationValidator : AbstractValidator<RegisterOrganization>
    {
        public RegisterOrganizationValidator()
        {
            RuleFor(c => c.Id).NotEmpty().Matches("Organizations-\\w");
            RuleFor(c => c.Name).NotEmpty().Length(2, 150);
            RuleFor(c => c.Address).NotEmpty().SetValidator(new AddressValidator());
        }
    }
}
