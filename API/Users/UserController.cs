﻿using Application.Authentication.Requests;
using Application.Authentication;
using Application.Users;
using Domain.Users;
using Microsoft.AspNetCore.Mvc;

namespace API.Users
{
    [ApiController]
    [Route("api/user")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IHashingService _hashingService;
        private readonly UserMapper _userMapper;

        public UserController(IUserRepository userRepository, ITokenService tokenService, IHashingService hashingService, UserMapper userMapper)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _hashingService = hashingService;
            _userMapper = userMapper;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegistrationRequest request)
        {
            if (await _userRepository.Get(request.Email) != null)
            {
                return Conflict(UserErrors.EmailNotUnique(request.Email));
            }

            var user = _userMapper.FromRegistrationRequestToModel(request);
            await _userRepository.Add(user);

            return Created("", new { id = user.Id });
        }


        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userRepository.Get(request.Email);
            if (user == null) return Unauthorized(UserErrors.NotFoundByEmail(request.Email));

            bool verified = _hashingService.Verify(request.Password, user.PasswordHash);
            if (!verified) return Unauthorized(UserErrors.WrongPassword(request.Email));

            string token = _tokenService.CreateToken(user);

            return Created("", new { token = token });
        }
    }
}
