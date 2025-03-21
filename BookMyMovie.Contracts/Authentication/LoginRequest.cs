namespace BookMyMovie.Contracts.Authentication;

public record LoginRequest(
    string EmailorUsername,
    string Password
    );