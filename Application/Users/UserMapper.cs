using Application.Authentication;
using Application.Authentication.Requests;

namespace Application.Users
{
    public class UserMapper
    {
        private readonly IHashingService _hashingService;

        public UserMapper(IHashingService hashingService)
        {
            _hashingService = hashingService;
        }

        public Domain.Users.User FromRegistrationRequestToModel(RegistrationRequest request)
        {
            return new Domain.Users.User(request.Email, _hashingService.Hash(request.Password), request.FirstName, request.LastName);
        }
    }
}
