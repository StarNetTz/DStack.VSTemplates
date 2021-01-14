using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Funq;
using ServiceStack;
using TemplateDomain.WebApi.ServiceInterface;
using TemplateDomain.Common;
using TemplateDomain.ReadModel;
using TemplateDomain.ReadModel.Queries.RavenDB;
using TemplateDomain.WebApi.Infrastructure;
using ServiceStack.Validation;
using ServiceStack.Auth;

namespace TemplateDomain.WebApi
{
    public class Startup : ModularStartup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public new void ConfigureServices(IServiceCollection services)
        {
            var store = new RavenDocumentStoreFactory().CreateAndInitializeDocumentStore(RavenConfig.FromConfiguration(Configuration));  // leave it here to avoid lazy loading until this is refactored so that this comment is NOT NEEDED
            services.AddSingleton(store);
            services.AddTransient<ITimeProvider, TimeProvider>();
            services.AddTransient<ITypeaheadSearchQuery, TypeaheadSearchQuery>();
            services.AddTransient<IMessageBus, NSBus>();
            services.AddTransient<IOrganizationSearchQuery, OrganizationSearchQuery>();
            services.AddTransient<IQueryById, QueryById>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseServiceStack(new AppHost
            {
                AppSettings = new NetCoreAppSettings(Configuration)
            });
        }
    }

    public class AppHost : AppHostBase
    {
        public AppHost() : base("TemplateDomain.WebApi", typeof(MyServices).Assembly)
        {
            //Licensing.RegisterLicense(Configuration["ServiceStack:Licence"]);
        }

        // Configure your AppHost with the necessary configuration and dependencies your App needs
        public override void Configure(Container container)
        {

            ServiceStack.Text.JsConfig.TreatEnumAsInteger = true;
            ServiceStack.Text.JsConfig.AssumeUtc = true;
            ServiceStack.Text.JsConfig.AlwaysUseUtc = true;
            ServiceStack.Text.JsConfig.DateHandler = ServiceStack.Text.DateHandler.ISO8601;

            SetConfig(new HostConfig
            {
                DefaultRedirectPath = "/metadata",
                DebugMode = AppSettings.Get(nameof(HostConfig.DebugMode), false)
            });

            Plugins.Add(new ValidationFeature());
            Plugins.Add(new CorsFeature(allowCredentials: true, allowedHeaders: "Content-Type, Authorization", allowOriginWhitelist: GetOriginWhiteList()));
            Plugins.Add(new AuthFeature(() => new AuthUserSession(),
                new IAuthProvider[] {
                new JwtAuthProvider(AppSettings) {
                    AuthKeyBase64 = Configuration["Jwt:Key"],
                    RequireSecureConnection = false, //dev configuration
                    EncryptPayload = false, //dev configuration
                    HashAlgorithm = "HS256"
                }
            }));
        }

            string[] GetOriginWhiteList()
                => Configuration["CORS:Whitelist"].Split(';');
    }
}