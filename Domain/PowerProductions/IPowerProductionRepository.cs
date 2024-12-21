
namespace Domain.PowerProductions
{
    public interface IPowerProductionRepository
    {
        Task<IEnumerable<PowerProduction>> GetByQuery(PowerProductionQuery query);
    }
}
