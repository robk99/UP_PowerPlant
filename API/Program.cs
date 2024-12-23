using Infrastructure;
using Infrastructure.Data;
using Application;
using API;
using Serilog;
using API.Infrastructure.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureLogging(builder.Configuration);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddPresentation();


var app = builder.Build();

#region HTTP Request Pipeline

app.MapHealthChecks("/api/health");

if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabase();
}

app.UseExceptionHandler();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
#endregion

app.Run();
