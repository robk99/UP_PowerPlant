using API.Authentication.Requests;
using Domain.Users;
using Infrastructure.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace API.Users
{
    [ApiController]
    [Route("api/user")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly TokenService _tokenService;

        public UserController(IUserRepository userRepository, TokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegistrationRequest request)
        {
            // TODO: Centralize mapping
            // TODO: Hash password
            var user = new User(request.Email, request.Password, request.FirstName, request.LastName);

            await _userRepository.Add(user);

            return Created("", new { id = user.Id });
        }


        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userRepository.Get(request.Email);
            if (user == null) return Unauthorized();

            // TODO: Hash password validation

            string token = _tokenService.CreateToken(user);

            return Created("", new { token = token });
        }
    }
}
