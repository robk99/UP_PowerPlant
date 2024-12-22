using Domain.PowerPlants;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.PowerPlants
{
    public class PowerPlantRepository : IPowerPlantRepository
    {
        private readonly AppDbContext _context;

        public PowerPlantRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<PowerPlant?> GetById(int id)
        {
            return await _context.PowerPlants.FindAsync(id);
        }

        public async Task<bool> Add(PowerPlant powerPlant)
        {
            _context.PowerPlants.Add(powerPlant);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Update(PowerPlant powerPlant)
        {
            var existingPowerPlant = await _context.PowerPlants
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == powerPlant.Id);

            if (existingPowerPlant == null || existingPowerPlant.IsDeleted)
                return false;

            _context.PowerPlants.Update(powerPlant);
            await _context.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Soft Delete
        /// </summary>
        /// <param name="id"></param>
        public async Task<bool> Delete(int id)
        {
            var owerPlant = await _context.PowerPlants.FindAsync(id);

            if (owerPlant == null || owerPlant.IsDeleted)
                return false;
            owerPlant.IsDeleted = true;

            _context.PowerPlants.Update(owerPlant);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
