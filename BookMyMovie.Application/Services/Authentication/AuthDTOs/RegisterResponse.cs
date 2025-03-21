namespace BookMyMovie.Application.Services.Authentication.AuthDTOs;

public record RegisterResponse(
    Guid UserId,
    string Email,
    string? PhoneNumber,
    string UserName,
    string FirstName,
    string LastName,
    string Role
);