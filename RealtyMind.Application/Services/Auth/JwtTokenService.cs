using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RealtyMind.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace RealtyMind.Application.Services.Auth
{
    public class JwtTokenService
    {
        private readonly string _key;
        private readonly string _issuer;
        private readonly string _audience;

        public JwtTokenService(IConfiguration config)
        {
            _key = config["Jwt:Key"]!;
            _issuer = config["Jwt:Issuer"]!;
            _audience = config["Jwt:Audience"]!;
        }

        public string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("role", user.Role)
        };

           var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
