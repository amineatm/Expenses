using Expenses.API.Data.Services.Authentication;
using Expenses.API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class AuthController(IAuthService _authService) : ControllerBase
{

    [HttpPost("Login")]
    public async Task<IActionResult> Login(UserLoginRequest request)
    {
        var token = await _authService.Login(request);
        return Ok(new { token });
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(UserRegistrationRequest request)
    {
        var token = await _authService.Register(request);
        return Ok(new { token });
    }

    [AllowAnonymous]
    [HttpDelete("DeleteAllUsers")]
    public async Task<IActionResult> DeleteAll()
    {
        await _authService.DeleteAll();
        return NoContent();
    }
}
