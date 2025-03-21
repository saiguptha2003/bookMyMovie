namespace BookMyMovie.Contracts.Authentication;

public record LoginResponse(
    Guid UserId,
    string Email,
    string UserName,
    string Token,
    string Role
    );