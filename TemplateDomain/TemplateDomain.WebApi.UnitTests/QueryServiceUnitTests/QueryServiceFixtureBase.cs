using TemplateDomain.WebApi.ServiceInterface;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Testing;
using ServiceStack.Validation;
using System;

namespace TemplateDomain.WebApi.UnitTests
{
    public abstract class QueryServiceFixtureBase<T> : IDisposable where T : Service
    {
        public T Service;
        protected ServiceStackHost AppHost;

        public abstract void RegisterServices(Funq.Container container);

        public QueryServiceFixtureBase()
        {
            ConfigureAppHost();
        }
            void ConfigureAppHost()
            {
                //LicenceRegistrator.RegisterServiceStackLicenceWhenNrOfServicesExceedesFreeThreshold();

                AppHost = new BasicAppHost(typeof(OrganizationService).Assembly)
                {
                    TestMode = true,
                    ConfigureContainer = container =>
                    {
                        container.Register<IAuthSession>(c => new AuthUserSession
                        {
                            Email = "mensad@mail.com"
                        });
                        container.RegisterValidators(typeof(AddressValidator).Assembly);
                        RegisterServices(container);
                    }
                };
                AppHost.Plugins.Add(new ValidationFeature());
                AppHost.Init();
                Service = AppHost.Container.Resolve<T>();
                Service.Request = new MockHttpRequest();
            }

        public void Dispose()
        {
            AppHost.Dispose();
        }
    }
}