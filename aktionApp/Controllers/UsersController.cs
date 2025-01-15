using aktionApp.DTOs;
using aktionApp.Entities;
using aktionApp.Entities.Interfaces;
using aktionApp.Repos;
using aktionApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aktionApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _userRepository;
        private readonly JwtServices _jwtService;

        //Konstruktor för att injicera beroenden
        public UsersController(IUsersRepository userRepository, JwtServices jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        //Endpoint för att registrera en ny användare
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserCreateDto userDto)
        {
            //Kontrollera om användarnamnet redan finns
            if (await _userRepository.GetUserByUsernameAsync(userDto.Username) != null)
            {
                return BadRequest("Användarnamnet är redan taget.");
            }

            //Skapa en ny användare och hash:a lösenordet
            var user = new User
            {
                Username = userDto.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password)
            };

            //Spara användaren i databasen
            await _userRepository.AddUserAsync(user);
            return Ok("Användare registrerad.");
        }

        //Endpoint för att logga in användaren
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto userDto)
        {
            //Verifiera användarens uppgifter
            var user = await _userRepository.AuthenticateAsync(userDto.Username, userDto.Password);
            if (user == null)
            {
                return Unauthorized("Ogiltigt användarnamn eller lösenord.");
            }

            //Generera JWT-token och returnera den
            var token = _jwtService.GenerateToken(user);
            return Ok(new { Token = token });
        }
    }
}
