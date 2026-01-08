using Expenses.API.Data;
using Expenses.API.DTOs;
using Expenses.API.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Expenses.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class AuthController(ExpensesDbContext dbContext, IConfiguration configuration, PasswordHasher<User> passwordHasher) : ControllerBase
    {
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            var user = dbContext.Users.FirstOrDefault(c => c.Email == request.Email);
            if (user is null) return Unauthorized("Invalid Credentials!");

            var password = passwordHasher.VerifyHashedPassword(user, user.Password, request.Password);

            if (password == PasswordVerificationResult.Failed) return Unauthorized("Invalid Credentials!");

            var token = GenerateJwtToken(user);

            return Ok(
                new
                {
                    Token = token
                }
            );
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
        {
            var userExists = dbContext.Users.Any(u => u.Email == request.Email);
            if (userExists) return BadRequest("User already exists");

            var user = new User
            {
                Email = request.Email,
                Password = passwordHasher.HashPassword(null, request.Password),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();

            return Ok(
                new
                {
                    token = GenerateJwtToken(user)
                }
            );
        }

        private string GenerateJwtToken(User user)
        {
            Claim[] claims =
            [
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            ];
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["AppSettings:Token"])
            );
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: configuration["AppSettings:Issuer"],
                audience: configuration["AppSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
