using aktionApp.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace aktionApp.Services
{
    public class JwtServices
    {
        private readonly string _secret;
        private readonly string _issuer;
        private readonly string _audience;

        //Läser konfiguration för nyckel och utgivare från appsettings.json
        public JwtServices(IConfiguration configuration)
        {
            _secret = configuration["Jwt:Secret"] ?? throw new ArgumentNullException("Jwt:Secret är inte konfigurerad.");
            _issuer = configuration["Jwt:Issuer"] ?? throw new ArgumentNullException("Jwt:Issuer är inte konfigurerad.");
            _audience = configuration["Jwt:Audience"] ?? throw new ArgumentNullException("Jwt:Audience är inte konfigurerad.");
        }

        // Genererar JWT-token för en användare
        public string GenerateToken(Users user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user), "Användarobjektet får inte vara null.");

            var claims = new[]
            {
               new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: creds
            );

            Console.WriteLine($"Token generated for user {user.Username}: {new JwtSecurityTokenHandler().WriteToken(token)}");
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
