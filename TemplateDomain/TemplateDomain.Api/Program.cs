using TemplateDomain.Api;
using TemplateDomain.ReadModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var a = typeof(TemplateDomain.Api.ServiceInterface.WeatherForecastController).Assembly;
builder.Services.AddControllers().AddApplicationPart(a).AddControllersAsServices();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(MockFactory.CreateQueryByIdMock());
builder.Services.AddSingleton(MockFactory.CreateOrganizationQueriesMock());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints( endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
