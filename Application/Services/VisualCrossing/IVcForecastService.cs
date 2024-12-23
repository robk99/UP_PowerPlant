
using Domain.PowerPlants;

namespace Application.Services.VisualCrossing
{
    public interface IVcForecastService
    {
        double GetForecastedProduction(double cloudcover, PowerPlant powerPlant);
    }
}
