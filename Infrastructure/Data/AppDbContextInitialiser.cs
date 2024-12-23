using Application.Authentication;
using Domain.Locations;
using Domain.PowerPlants;
using Domain.PowerProductions;
using Domain.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public static class InitialiserExtensions
    {
        public static async Task InitialiseDatabase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var initialiser = scope.ServiceProvider.GetRequiredService<AppDbContextInitialiser>();

            await initialiser.ApplyMigrations();
            await initialiser.SeedDB();
        }
    }

    public class AppDbContextInitialiser
    {
        private readonly AppDbContext _context;
        private readonly IHashingService _hashingService;
        private readonly ILogger<AppDbContextInitialiser> _logger;

        public AppDbContextInitialiser(AppDbContext context, IHashingService hashingService, ILogger<AppDbContextInitialiser> logger)
        {
            _context = context;
            _hashingService = hashingService;
            _logger = logger;
        }

        public async Task ApplyMigrations()
        {
            try
            {
                if (_context.Database.GetPendingMigrations().Any())
                {
                    Console.WriteLine("----------------- APPLYING DB MIGRATIONS -----------------");
                    await _context.Database.MigrateAsync();
                }
                else
                {
                    Console.WriteLine("----------------- NO DB MIGRATIONS -----------------");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initializing the database.");
                throw;
            }
        }

        public async Task SeedDB()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
            }
        }

        public async Task TrySeedAsync()
        {
            // check if the database is already seeded
            var defaultUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == "admin@admin.com");
            if (defaultUser != null) return;

            var rnd = new Random();

            var powerPlants = new List<PowerPlant>();
            var powerProductions = new List<PowerProduction>();
            var powerProdctionId = 1;

            // Power Plants and Power Productions
            for (int i = 1; i <= 50; i++)
            {
                var installationDate = DateTime.Now.AddDays(-rnd.Next(0, 10));
                var powerPlant = new PowerPlant(
                    rnd.Next(1, 99),
                    installationDate,
                    new Location((double)rnd.Next(-90, 90), (double)rnd.Next(-180, 180)),
                    $"Power Plant {i}"
                );

                powerPlants.Add(powerPlant);

                // Generate Power Production for each 15 minutes since the Installation Date
                var startTime = RoundToNearestQuarter(installationDate);
                var endTime = DateTime.Now;

                while (startTime <= endTime)
                {
                    powerProductions.Add(new PowerProduction
                    (
                        rnd.Next(1, 100), startTime, i
                    ));
                    powerProdctionId++;

                    startTime = startTime.AddMinutes(15);
                }
            }

            var adminUser = new User("admin@admin.com", "admin", "adminFN", "adminLN");

            _context.PowerPlants.AddRange(powerPlants);
            _context.PowerProductions.AddRange(powerProductions);
            _context.Users.Add(adminUser);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Helper method used only here for rounding a ProductionPower's  DateTime to the nearest quarter
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private DateTime RoundToNearestQuarter(DateTime dateTime)
        {
            var minutes = (dateTime.Minute / 15) * 15; // Round down to the nearest 15-minute block
            var roundedTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, minutes, 0);
            return roundedTime;
        }
    }
}
