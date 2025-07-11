using API.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly string testUser = "admin";
        private readonly string testPassword = "1234";
        private readonly string jwtSecret = "EstaEsUnaClaveSuperSeguraJWT123456789!";

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest1 request)
        {
            if (request.Username == testUser && request.Password == testPassword)
            {
                var token = GenerateJwtToken(request.Username);
                return Ok(new LoginResponse { Token = token });
            }
            return Unauthorized();
        }

        private string GenerateJwtToken(string username)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "tu-app",
                audience: "tu-app",
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}