using FluentValidation.AspNetCore;
using Microsoft.IdentityModel.Tokens;
using TemplateDomain.Api.Impl;
using TemplateDomain.Api.ServiceInterface;
using TemplateDomain.Common;
using TemplateDomain.ReadModel;
using TemplateDomain.ReadModel.Queries.RavenDB;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureAppConfiguration(bld =>
{
    bld.AddJsonFile("config/appsettings.json", optional: false);
});

// Add services to the container.

var a = typeof(OrganizationQueryController).Assembly;
builder.Services.AddControllers()
    .AddApplicationPart(a)
    .AddControllersAsServices()
    .AddFluentValidation(fvc=>fvc.RegisterValidatorsFromAssembly(a));


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



#region AddServices

builder.Services.AddTransient<ITimeProvider, TimeProvider>();
builder.Services.AddTransient<IMessageBus, NSBus>();

var store = new RavenDocumentStoreFactory().CreateAndInitializeDocumentStore(RavenConfig.FromConfiguration(builder.Configuration));  // leave it here to avoid lazy loading until this is refactored so that this comment is NOT NEEDED
builder.Services.AddSingleton(store);
builder.Services.AddTransient<ITypeaheadQueries, TypeaheadQueries>();
builder.Services.AddTransient<IOrganizationQueries, OrganizationQueries>();
builder.Services.AddTransient<IQueryById, QueryById>();
#endregion

builder.Services.AddAutoMapper(typeof(CommandsProfile).Assembly);
var app = builder.Build();

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

app.UseEndpoints( endpoints =>
{
    endpoints.MapControllers().RequireAuthorization("ApiScope");
});

app.Run();
