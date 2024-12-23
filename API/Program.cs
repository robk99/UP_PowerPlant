using API.Extensions;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddHealthChecks();

var app = builder.Build();


#region HTTP Request Pipeline
app.MapHealthChecks("/api/health");

if (app.Environment.IsDevelopment())
{
    app.ApplyMigrations();
}

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
#endregion

app.Run();
