using TemplateDomain.WebApi.ServiceInterface;
using Moq;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Testing;
using ServiceStack.Validation;
using System;
using Xunit;
using TemplateDomain.Testing;

namespace TemplateDomain.WebApi.UnitTests;

[CollectionDefinition("AppHost collection")]
public class AppHostCollection : ICollectionFixture<AppHostFixture>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}

public class AppHostFixture : IDisposable
{
    public ServiceStackHost AppHost { get; private set; }

    public AppHostFixture()
    {
        //Licensing.RegisterLicense("");
        AppHost = new BasicAppHost(typeof(OrganizationService).Assembly)
        {
            TestMode = true,
            ConfigureContainer = container =>
            {
                container.Register(new Mock<IMessageBus>().Object);
                container.Register(new MockTimeProvider());
                container.Register<IAuthSession>(c => new AuthUserSession
                {
                    Email = "admin@mail.com"
                });
                container.RegisterValidators(typeof(AddressValidator).Assembly);
            }
        };
        AppHost.Plugins.Add(new ValidationFeature());
        AppHost.Init();
    }

    public void Dispose()
    {
        AppHost.Dispose();
    }
}
