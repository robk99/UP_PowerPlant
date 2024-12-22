
using Domain.PowerPlants;

namespace Domain.Users
{
    public interface IUserRepository
    {
        Task<User?> Get(string email);
        Task<bool> Add(User user);
        Task<bool> Update(User user);
    }
}
