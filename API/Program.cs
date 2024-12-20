using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddHealthChecks();

var app = builder.Build();


#region HTTP Request Pipeline
app.MapHealthChecks("/api/health");

app.UseAuthorization();

app.MapControllers();
#endregion

app.Run();
