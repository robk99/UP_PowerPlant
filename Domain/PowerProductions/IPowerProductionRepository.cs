
using Domain.PowerProductions.Queries;

namespace Domain.PowerProductions
{
    public interface IPowerProductionRepository
    {
        Task<IEnumerable<PowerProduction>> GetTimeseries(PowerProductionTimeseriesQuery query);
    }
}
