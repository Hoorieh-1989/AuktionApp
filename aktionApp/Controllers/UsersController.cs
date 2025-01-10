using aktionApp.DTOs;
using aktionApp.Entities.Interfaces;
using aktionApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aktionApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUsersRepository _userRepository;
        private readonly JwtServices _jwtService;

        public UserController(IUsersRepository userRepository, JwtServices jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UsersDto userDto)
        {
            var user = await _userRepository.AuthenticateAsync(userDto.Username, userDto.Password);
            if (user == null) return Unauthorized("Invalid username or password");

            var token = _jwtService.GenerateToken(user);
            return Ok(new { Token = token });
        }
    }
}
