
using Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Domain.PowerPlants;
using Infrastructure.PowerPlants;
using Infrastructure.PowerProductions;
using Domain.PowerProductions;
using Infrastructure.Users;
using Domain.Users;
using Infrastructure.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Application.Authentication;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddScoped<ISaveChangesInterceptor, UpdateAuditDetailsInterceptor>();
            services.AddDbContext<AppDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseSqlServer(connectionString);
            });
            
            services
                .AddScoped<IPowerPlantRepository, PowerPlantRepository>()
                .AddScoped<IPowerProductionRepository, PowerProductionRepository>()
                .AddScoped<IUserRepository, UserRepository>();

            services.AddSingleton<IHashingService, HashingService>();

            services
                .AddSingleton<ITokenService, TokenService>()
                .AddJWTAuthentication(configuration)
                .AddAuthorization();

            return services;
        }

        private static IServiceCollection AddJWTAuthentication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!)),
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        ClockSkew = TimeSpan.Zero
                    };
                });

            return services;
        }
    }
}
