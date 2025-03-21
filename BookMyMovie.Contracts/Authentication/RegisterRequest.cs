namespace BookMyMovie.Contracts.Authentication;

public record RegisterRequest(
    string UserName,
    string Password,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Location
    );