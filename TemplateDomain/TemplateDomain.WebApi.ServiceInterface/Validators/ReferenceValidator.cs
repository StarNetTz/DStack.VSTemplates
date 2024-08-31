using ServiceStack.FluentValidation;

namespace TemplateDomain.WebApi.ServiceInterface;

public class ReferenceValidator : AbstractValidator<Ref>
{
    public ReferenceValidator()
    {
        RuleFor(c => c.Id).NotEmpty().Length(2, 255);
        RuleFor(c => c.Val).NotEmpty().Length(1, 255);
    }
}