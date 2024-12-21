
using Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Domain.PowerPlants;
using Infrastructure.PowerPlants;
using Infrastructure.PowerProductions;
using Domain.PowerProductions;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AppDbContext>((sp, options) =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IPowerPlantRepository, PowerPlantRepository>();
            services.AddScoped<IPowerProductionRepository, PowerProductionRepository>();

            return services;
        }
    }
}
