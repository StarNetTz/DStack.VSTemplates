﻿using TemplateDomain.Domain.Organization;

namespace TemplateDomain.Domain.NSBus;

public class RegisterOrganizationHandler : AggregateHandlerBase, IHandleMessages<RegisterOrganization>
{
    readonly IOrganizationInteractor Svc;

    public RegisterOrganizationHandler(IOrganizationInteractor svc)
        => Svc = svc;

    public async Task Handle(RegisterOrganization message, IMessageHandlerContext context)
        => await TryHandle(message, context, Svc);
}