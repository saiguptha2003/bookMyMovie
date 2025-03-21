using System.ComponentModel.DataAnnotations;

namespace BookMyMovie.Contracts.Authentication;

public record RegisterResponse
(
    Guid UserId,
    string Email,
    string? PhoneNumber,
    string FirstName,
    string LastName,
    string UserName
);