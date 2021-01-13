using ServiceStack.FluentValidation;
using TemplateDomain.Common;

namespace TemplateDomain.WebApi.ServiceInterface
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(c => c.Street).NotEmpty().NotEmpty().Length(2, 150);
            RuleFor(c => c.City).NotEmpty().Length(2, 150);
            RuleFor(c => c.State).NotEmpty().Length(2, 150);
            RuleFor(c => c.PostalCode).NotEmpty().Length(2, 150);
            RuleFor(c => c.Country).NotEmpty().Length(2, 150);
        }
    }
}
