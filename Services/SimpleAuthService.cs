using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
namespace BlogApp.Services
{
    public class SimpleAuthService
    {
        private readonly IConfiguration _configuration;

        public SimpleAuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Returns the JWT token string if successful, otherwise null
        public Task<string?> LoginAsync(string username, string password)
        {
            if (username.ToLower() == "admin" && password == "password123") // Hardcoded for demo
            {
                var token = GenerateJwtToken(username);
                return Task.FromResult<string?>(token);
            }
            return Task.FromResult<string?>(null);
        }

        private string GenerateJwtToken(string username)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "Admin") // Adds Admin role to the token claims
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpiryInMinutes"]!)),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}