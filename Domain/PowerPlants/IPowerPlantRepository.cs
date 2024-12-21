
namespace Domain.PowerPlants
{
    public interface IPowerPlantRepository
    {
        Task<PowerPlant?> GetById(int id);
        Task<bool> Add(PowerPlant powerPlant);
        Task<bool> Update(PowerPlant powerPlant);
        /// <summary>
        /// Soft Delete
        /// </summary>
        /// <param name="id"></param>
        Task<bool> Delete(int id);
    }
}
