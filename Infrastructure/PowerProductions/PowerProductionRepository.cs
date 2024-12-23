using Domain.PowerProductions;
using Domain.PowerProductions.Queries;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.PowerProductions
{
    public class PowerProductionRepository : IPowerProductionRepository
    {
        private readonly AppDbContext _context;

        public PowerProductionRepository(AppDbContext context)
        {
            _context = context;

        }
        public async Task<IEnumerable<PowerProduction>> GetTimeseries(PowerProductionTimeseriesQuery query)
        {
            return await _context.PowerProductions
                .Where(o => (o.PowerPlantId == query.PowerPlantId) &&
                            (o.Timestamp >= query.StartDate) &&
                            (o.Timestamp < query.EndDate))
                .ToListAsync();
        }
    }
}
