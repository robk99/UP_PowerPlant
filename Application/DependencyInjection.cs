using Application.PowerPlants;
using Application.PowerProductions;
using Application.Services;
using Application.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<TimeseriesService>();
            services.AddScoped<PowerPlantMapper>();
            services.AddScoped<PowerProductionMapper>();
            services.AddScoped<UserMapper>();

            return services;
        }
    }
}
