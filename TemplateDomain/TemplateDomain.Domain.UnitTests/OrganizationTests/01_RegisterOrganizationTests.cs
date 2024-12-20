﻿using TemplateDomain.Domain.Organization;

namespace TemplateDomain.Domain.UnitTests.OrganizationTests;

public class RegisterOrganizationTests : AggregateTester<OrganizationInteractor>
{
    [Fact]
    public async Task Should_Execute_Command()
    {
        string aggId = $"{Consts.IdPrefixes.Organization}1";
        Given();
        When(OrganizationCommandsFactory.CreateRegisterOrganizationCommand(aggId));
        await Expect(OrganizationEventsFactory.CreateOrganizationRegisteredEvent(aggId));
    }

    [Fact]
    public async Task Command_Is_Idempotent()
    {
        string aggId = $"{Consts.IdPrefixes.Organization}1";
        var cmd = OrganizationCommandsFactory.CreateRegisterOrganizationCommand(aggId);
        var evt = OrganizationEventsFactory.CreateOrganizationRegisteredEvent(aggId);

        Given(evt);
        When(cmd);
        await ExpectNoEvents();
    }

    [Fact]
    public async Task NonIdempotent_Command_Should_Throw_DomainError()
    {
        string aggId = $"{Consts.IdPrefixes.Organization}1";
        var cmd = OrganizationCommandsFactory.CreateRegisterOrganizationCommand(aggId);
        cmd.Address.Country = new Starnet.Common.Ref { Id = "DE" , Val = "Germany" };
        var evt = OrganizationEventsFactory.CreateOrganizationRegisteredEvent(aggId);

        Given(evt);
        When(cmd);
        await ExpectError("OrganizationAlreadyExists");
    }
}