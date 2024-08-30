using TemplateDomain.Domain.Organization;
using TemplateDomain.PL.Commands;
using Moq;
using NServiceBus.Testing;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using DStack.Aggregates;
using System;
using TemplateDomain.Testing.PL;
using TemplateDomain.Common;

namespace TemplateDomain.Domain.NSBus.Tests;

public class RegisterOrganizationHandlerTests
{
    [Fact]
    public async Task Should_Handle_And_Publish_Any_Produced_PublishEvents()
    {
        var handler = new RegisterOrganizationHandler(CreateMockThatReturnsNoPublishedEvents());
        var context = new TestableMessageHandlerContext();
        await handler.Handle(OrganizationCommandsFactory.CreateRegisterOrganizationCommand("Orgs-1"), context);
    }

        static IOrganizationInteractor CreateMockThatReturnsNoPublishedEvents()
        {
            var mock = new Mock<IOrganizationInteractor>();
            mock.Setup(svc => svc.GetPublishedEvents()).Returns(new List<object>() { new object() });
            var mockObject = mock.Object;
            return mockObject;
        }

    [Fact]
    public async Task Should_Log_And_Ignore_DomainError()
    {
        var handler = new RegisterOrganizationHandler(CreateMockThatThrowsDomainError());
        var context = new TestableMessageHandlerContext();
        await handler.Handle(OrganizationCommandsFactory.CreateRegisterOrganizationCommand("Orgs-1"), context);
    }

        static IOrganizationInteractor CreateMockThatThrowsDomainError()
        {
            var mock = new Mock<IOrganizationInteractor>();
            mock.Setup(svc => svc.ExecuteAsync(It.IsAny<object>())).Throws<DomainError>();
            var mockObject = mock.Object;
            return mockObject;
        }
    [Fact]
    public async Task Should_Log_And_Rethrow_Any_Non_DomainError()
    {
        var handler = new RegisterOrganizationHandler(CreateMockThatThrowsArgumentException());
        var context = new TestableMessageHandlerContext();
        await Assert.ThrowsAnyAsync<ArgumentException>(async () => await handler.Handle(OrganizationCommandsFactory.CreateRegisterOrganizationCommand($"{Consts.IdPrefixes.Organization}1"), context).ConfigureAwait(false));
    }

        static IOrganizationInteractor CreateMockThatThrowsArgumentException()
        {
            var mock = new Mock<IOrganizationInteractor>();
            mock.Setup(svc => svc.ExecuteAsync(It.IsAny<object>())).Throws<ArgumentException>();
            var mockObject = mock.Object;
            return mockObject;
        }
}