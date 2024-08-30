using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TemplateDomain.Api.ServiceModel;
using TemplateDomain.Api.ServiceModel.Commands;
using TemplateDomain.Common;

namespace TemplateDomain.Api.ServiceInterface;


[Route("commands/organization")]
[ApiController]
[Authorize]
public class OrganizationCommandController : CommandControllerBase
{
    public OrganizationCommandController(IMessageSession bus, ITimeProvider timeProvider, IMapper mapper) : base(bus, timeProvider, mapper)
    {
    }

    [HttpPost("register")]
    public async Task Register([FromBody]RegisterOrganization cmd)
    {
        await MapAndProcessRequest<PL.Commands.RegisterOrganization>(cmd);
    }
}

public class RegisterOrganizationValidator : AbstractValidator<RegisterOrganization>
{
    public RegisterOrganizationValidator()
    {
        RuleFor(c => c.Id).NotEmpty().Matches("Organizations-\\w");
        RuleFor(c => c.Name).NotEmpty().Length(2, 150);
        RuleFor(c => c.Address).NotEmpty().SetValidator(new AddressValidator());
    }
}