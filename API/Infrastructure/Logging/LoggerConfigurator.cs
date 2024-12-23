using Serilog;
namespace API.Infrastructure.Logging
{
    public static class LoggerConfigurator
    {
        public static void ConfigureLogging(this IHostBuilder hostBuilder, IConfiguration configuration)
        {
            // Define the log location through environment variable 
            var logPath = Environment.GetEnvironmentVariable("LOG_PATH") ?? "Logs";
            hostBuilder.UseSerilog((context, loggerConfig) =>
                {
                    loggerConfig
                        .ReadFrom.Configuration(context.Configuration)
                        .WriteTo.File(
                        $"{logPath}/log-.txt",
                        rollingInterval: RollingInterval.Day,
                        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Warning,
                        outputTemplate: "{Timestamp:yyyy-MM-dd} [{Level}] {Message}{NewLine}{Exception}");
                });
        }
    }
}
