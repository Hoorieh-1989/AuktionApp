using aktionApp.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace aktionApp.Services
{
    public class JwtServices
    {
        private readonly string _key;
        private readonly string _issuer;

        //Läser konfiguration för nyckel och utgivare från appsettings.json
        public JwtServices(IConfiguration configuration)
        {
            _key = configuration["Jwt:Key"];
            _issuer = configuration["Jwt:Issuer"];
        }

        // Genererar JWT-token för en användare
        public string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Skapar nyckel och signeringsuppgifter för token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Skapar JWT-token med specifik giltighetstid
            var token = new JwtSecurityToken(
                _issuer,
                _issuer,
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds
            );

            // Returnerar token som sträng
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
