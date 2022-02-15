using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Funq;
using ServiceStack;
using $ext_projectname$.WebApi.ServiceInterface;
using $ext_projectname$.ReadModel.Queries.RavenDB;
using ServiceStack.Validation;
using ServiceStack.Auth;
using $ext_projectname$.Common;
using $ext_projectname$.ReadModel;
using $ext_projectname$.WebApi.Infrastructure;

[assembly: HostingStartup(typeof($ext_projectname$.WebApi.AppHost))]
namespace $ext_projectname$.WebApi;

public class AppHost : AppHostBase, IHostingStartup
    {
        public void Configure(IWebHostBuilder builder) =>
            builder.ConfigureServices((ctx, services) =>
            {

                var store = new RavenDocumentStoreFactory().CreateAndInitializeDocumentStore(RavenConfig.FromConfiguration(ctx.Configuration));  // leave it here to avoid lazy loading until this is refactored so that this comment is NOT NEEDED
                services.AddSingleton(store);
                services.AddTransient<ITimeProvider, TimeProvider>();
                services.AddTransient<ITypeaheadSearchQuery, TypeaheadSearchQuery>();
                services.AddTransient<IMessageBus, NSBus>();
                services.AddTransient<IOrganizationSearchQuery, OrganizationSearchQuery>();
                services.AddTransient<IQueryById, QueryById>();

                // Configure ASP.NET Core IOC Dependencies
            })
            .Configure(app =>
            {
                // Configure ASP.NET Core App
                if (!HasInit)
                    app.UseServiceStack(new AppHost());

            });

        public AppHost() : base("$ext_projectname$.WebApi", typeof(MyServices).Assembly)
        {
            ServiceStack.Text.JsConfig.TreatEnumAsInteger = true;
            ServiceStack.Text.JsConfig.AssumeUtc = true;
            ServiceStack.Text.JsConfig.AlwaysUseUtc = true;
            ServiceStack.Text.JsConfig.DateHandler = ServiceStack.Text.DateHandler.ISO8601;
        }

        public override void Configure(Container container)
        {
            // Configure ServiceStack only IOC, Config & Plugins
            SetConfig(new HostConfig
            {
              //  DefaultRedirectPath = "/metadata",
                UseSameSiteCookies = true
            });
            ConfigureCORS();
            Plugins.Add(new ValidationFeature());
            Plugins.Add(new AuthFeature(() => new AuthUserSession(),
                    new IAuthProvider[] {
                    new JwtAuthProvider(AppSettings) {
                        AuthKeyBase64 =  AppSettings.GetString("Jwt:Key"),
                        RequireSecureConnection = false, //dev configuration
                        EncryptPayload = false, //dev configuration
                        HashAlgorithm = "HS256"
                    }
                }));

    }

        void ConfigureCORS()
        {
            var str = AppSettings.GetString("CORS:Whitelist");
            if (!string.IsNullOrWhiteSpace(str))
                if (str == "*")
                    Plugins.Add(new CorsFeature());
                else
                    Plugins.Add(new CorsFeature(allowCredentials: true, allowedHeaders: "Content-Type, Authorization", allowOriginWhitelist: str.Split(';')));
        }
    }
