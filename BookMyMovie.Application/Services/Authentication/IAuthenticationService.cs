
using BookMyMovie.Application.Services.Authentication.AuthDTOs;

namespace BookMyMovie.Application.Services.Authentication;

public interface IAuthenticationService
{
     public Task<RegisterResponse> RegisterAsync(
        string username, 
        string password,
        string email,
        string firstName,
        string lastName,
        string phoneNumber,
        string location
        );

        public Task<LoginResponse> LoginAsync(
        string emailOrUsername,
        string password
    );
}