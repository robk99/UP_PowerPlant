
namespace Domain.PowerPlants
{
    public interface IPowerPlantRepository
    {
        Task<PowerPlant?> GetById(int id);
        Task<bool> Add(PowerPlant powerPlant);
        Task<bool> Update(PowerPlant powerPlant);
        Task<bool> Delete(int id);
    }
}
