using ServiceStack.FluentValidation;
using TemplateDomain.Common;

namespace TemplateDomain.WebApi.ServiceInterface;

public class AddressValidator : AbstractValidator<Address>
{
    public AddressValidator()
    {
        RuleFor(c => c.Street).NotEmpty().NotEmpty().Length(2, 150);
        RuleFor(c => c.City).NotEmpty().Length(2, 150);
        RuleFor(c => c.State).NotEmpty().Length(2, 150);
        RuleFor(c => c.PostalCode).NotEmpty().Length(2, 150);
        RuleFor(c => c.Country).NotEmpty().SetValidator(new ReferenceValidator());
    }
}

public class ReferenceValidator : AbstractValidator<Ref>
{
    public ReferenceValidator()
    {
        RuleFor(c => c.Id).NotEmpty().Length(2, 255);
        RuleFor(c => c.Val).NotEmpty().Length(1, 255);
    }
}