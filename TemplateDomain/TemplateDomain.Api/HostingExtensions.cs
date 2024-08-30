using TemplateDomain.Api.Impl;
using TemplateDomain.ReadModel.Queries.RavenDB;
using TemplateDomain.ReadModel;
using FluentValidation.AspNetCore;
using Microsoft.IdentityModel.Tokens;
using TemplateDomain.Api.ServiceInterface;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using FluentValidation;

namespace TemplateDomain.Api
{
    public static class HostingExtensions
    {
        const string CorsPolicyName = "CorsPolicy";
        const string ApiScopePolicyName = "ApiScope";
        const string NpgCnnKey = "PostgreSQL:ConnectionString";
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<ITimeProvider, StarnetTimeProvider>();
            builder.Services.AddTransient<IMessageBus, NSBus>();

            var store = new RavenDocumentStoreFactory().CreateAndInitializeDocumentStore(RavenConfig.FromConfiguration(builder.Configuration));  // leave it here to avoid lazy loading until this is refactored so that this comment is NOT NEEDED
            builder.Services.AddSingleton(store);
            builder.Services.AddTransient<ITypeaheadQueries, TypeaheadQueries>();
            builder.Services.AddTransient<IOrganizationQueries, OrganizationQueries>();
            builder.Services.AddTransient<IQueryById, QueryById>();
            builder.Services.AddAutoMapper(typeof(CommandsProfile).Assembly);


            var a = typeof(OrganizationQueryController).Assembly;
            builder.Services.AddControllers()
                .AddApplicationPart(a)
                .AddControllersAsServices();

            builder.Services.AddValidatorsFromAssemblyContaining<RegisterOrganizationValidator>()
            .AddFluentValidationAutoValidation(cfg => {
                cfg.DisableBuiltInModelValidation = true;
            });


            builder.Services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = "https://localhost:5001";

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false
                };
            });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "raapi");
                });
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            return builder.Build();
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireAuthorization("ApiScope");
            });

            return app;
        }
    }

}
