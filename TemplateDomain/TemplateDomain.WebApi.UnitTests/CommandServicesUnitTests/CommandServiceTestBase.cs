using TemplateDomain.WebApi.ServiceInterface;
using Microsoft.Extensions.Configuration;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Testing;
using ServiceStack.Validation;
using System;
using Moq;
using TemplateDomain.Common;

namespace TemplateDomain.WebApi.UnitTests
{
    public class CommandServiceTestBase<T> : IDisposable where T : Service
    {
        public T Service { get; set; }

        protected ServiceStackHost AppHost;

        public CommandServiceTestBase()
        {
            ConfigureAppHost();
        }

        public void ConfigureAppHost()
        {
            //LicenceRegistrator.RegisterServiceStackLicenceWhenNrOfServicesExceedesFreeThreshold();

            AppHost = new BasicAppHost(typeof(OrganizationService).Assembly)
            {
                TestMode = true,
                ConfigureContainer = container =>
                {
                    container.Register(new Mock<IMessageBus>().Object);
                    container.Register(CreateStubTimeProvider());
                    container.Register<IAuthSession>(c => new AuthUserSession
                    {
                        Email = "admin@mail.com"
                    });
                    container.RegisterValidators(typeof(AddressValidator).Assembly);
                }
            };
            AppHost.Plugins.Add(new ValidationFeature());
            AppHost.Init();
            Service = AppHost.Container.Resolve<T>();
            Service.Request = new MockHttpRequest();
        }

        public ITimeProvider CreateStubTimeProvider()
        {
            var m = new Mock<ITimeProvider>();
            m.Setup(x => x.GetUtcTime()).Returns(DateTime.MinValue);
            return m.Object;
        }

        public void Dispose()
        {
            AppHost.Dispose();
        }
    }
}