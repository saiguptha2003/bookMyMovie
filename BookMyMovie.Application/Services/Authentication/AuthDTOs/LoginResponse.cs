namespace BookMyMovie.Application.Services.Authentication.AuthDTOs;

public record LoginResponse(
    Guid UserId,
    string Email,
    string UserName,
    string Token,
    string Role
    );