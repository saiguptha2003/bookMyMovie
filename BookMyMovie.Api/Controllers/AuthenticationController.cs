using BookMyMovie.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;
using BookMyMovie.Application.Services.Authentication;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace BookMyMovie.Api.Controllers;

[ApiController]
[Route("/api/auth")]
public class AuthenticationController(IAuthenticationService authenticationService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
    {
        var register = await authenticationService.RegisterAsync(
            registerRequest.UserName,
            registerRequest.Password,
            registerRequest.Email,
            registerRequest.FirstName,
            registerRequest.LastName,
            registerRequest.PhoneNumber,
            registerRequest.Location
        );

        var response = new RegisterResponse(
            register.UserId,
            register.Email,
            register.PhoneNumber,
            register.UserName,
            register.FirstName,
            register.LastName
        );

        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var login = await authenticationService.LoginAsync(
            loginRequest.EmailorUsername,
            loginRequest.Password
        );

        var response = new LoginResponse(
            login.UserId,
            login.Email,
            login.UserName,
            login.Token,
            login.Role
        );

        return Ok(response);
    }

    // Secure route to get current user's profile
    [Authorize]
    [HttpGet("profile")]
    public IActionResult GetProfile()
    {
        var userId = User.FindFirst("id")?.Value;
        var username = User.Identity?.Name;
        var role = User.FindFirst("role")?.Value;

        return Ok(new
        {
            UserId = userId,
            Username = username,
            Role = role
        });
    }
};