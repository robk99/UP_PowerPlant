using Domain.Users;

namespace Application.Authentication
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
