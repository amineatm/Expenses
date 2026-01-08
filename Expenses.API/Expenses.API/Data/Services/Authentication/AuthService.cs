using Expenses.API.Data;
using Expenses.API.Data.Services.Authentication;
using Expenses.API.DTOs;
using Expenses.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class AuthService(
    ExpensesDbContext dbContext,
    IConfiguration configuration,
    PasswordHasher<User> passwordHasher) : IAuthService
{
    private readonly ExpensesDbContext _dbContext = dbContext;
    private readonly IConfiguration _configuration = configuration;
    private readonly PasswordHasher<User> _passwordHasher = passwordHasher;

    public async Task DeleteAll()
    {
        await _dbContext.Users.ExecuteDeleteAsync();
    }

    public async Task<string> Login(UserLoginRequest request)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.Email == request.Email);
        if (user == null)
            throw new UnauthorizedAccessException("Invalid Credentials!");

        var result = _passwordHasher.VerifyHashedPassword(
            user, user.Password, request.Password);

        if (result == PasswordVerificationResult.Failed)
            throw new UnauthorizedAccessException("Invalid Credentials!");

        return GenerateJwtToken(user);
    }

    public async Task<string> Register(UserRegistrationRequest request)
    {
        if (_dbContext.Users.Any(u => u.Email == request.Email))
            throw new InvalidOperationException("User already exists");

        var user = new User
        {
            Email = request.Email,
            Password = _passwordHasher.HashPassword(null, request.Password),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();

        return GenerateJwtToken(user);
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["AppSettings:Token"])
        );

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["AppSettings:Issuer"],
            audience: _configuration["AppSettings:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
