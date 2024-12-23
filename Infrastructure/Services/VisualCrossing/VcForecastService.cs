using Application.Services.VisualCrossing;
using Domain.PowerPlants;

namespace Infrastructure.Services.VisualCrossing
{
    public class VcForecastService : IVcForecastService
    {
        public double GetForecastedProduction(double cloudcover, PowerPlant powerPlant)
        {
            // WARNING: business logic faked

            return cloudcover switch
            {
                > 80 => powerPlant.InstalledPower * 0.3,
                > 50 => powerPlant.InstalledPower * 0.6,
                < 10 => powerPlant.InstalledPower * 0.95,
                _ => powerPlant.InstalledPower * 0.8
            };
        }
    }
}
