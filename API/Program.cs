using Infrastructure;
using Infrastructure.Data;
using Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddHealthChecks();

var app = builder.Build();


#region HTTP Request Pipeline
app.MapHealthChecks("/api/health");

if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabase();
}

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
#endregion

app.Run();
