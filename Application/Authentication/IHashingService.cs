
namespace Application.Authentication
{
    public interface IHashingService
    {
        string Hash(string password);

        bool Verify(string password, string passwordHash);
    }
}
