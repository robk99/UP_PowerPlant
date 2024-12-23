using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            if (dbContext.Database.GetPendingMigrations().Any())
            {
                Console.WriteLine("----------------- APPLYING DB MIGRATIONS -----------------");
                dbContext.Database.Migrate();
            }
            else
            {
                Console.WriteLine("----------------- NO DB MIGRATIONS -----------------");
            }
        }
    }
}
