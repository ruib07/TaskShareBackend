using TaskShare.Services;
using TaskShare.API.Configurations.Documentation;
using TaskShare.API.Configurations.Persistance;
using TaskShare.API.Configurations.Security;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddCustomApiDocumentation();
builder.Services.AddCustomApiSecurity(configuration);
builder.Services.AddCustomServiceDependencyRegister(configuration);
builder.Services.AddCustomDatabaseConfiguration(configuration);

var app = builder.Build();

app.UseCustomApiDocumentation();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
