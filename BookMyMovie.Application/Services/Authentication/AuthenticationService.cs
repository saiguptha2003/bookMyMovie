using System.Security.Cryptography;
using System.Text;
using BookMyMovie.Application.Common.Authentication;
using BookMyMovie.Application.Common.Persistence;
using BookMyMovie.Application.Services.Authentication.AuthDTOs;
using BookMyMovie.Domain.Entities;

namespace BookMyMovie.Application.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<RegisterResponse> RegisterAsync(string userName, string password, string email, string firstName, string lastName,
            string phoneNumber, string location)
        {
            string hashedPassword = HashPassword(password);

            // Check if user already exists
            var existingUser = await _userRepository.GetUserByEmailOrUsernameAsync(email);
            if (existingUser != null)
            {
                throw new Exception("User with this email or username already exists.");
            }

            // Create new user
            var user = new User
            {
                Username = userName,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                Role = "Ticketee",
                PhoneNumber = phoneNumber,
                Location = location,
                Password = hashedPassword,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            Console.WriteLine("Adding user...");
            await _userRepository.AddAsync(user); // FIXED: Await the async method
            Console.WriteLine($"User registered: {user.Email}, ID: {user.Id}");

            return new RegisterResponse(
                user.Id,
                user.Email,
                user.PhoneNumber,
                user.Username,
                user.FirstName,
                user.LastName,
                user.Role
            );
        }

        public async Task<LoginResponse> LoginAsync(string emailOrUsername, string password)
        {
            

            // 1. Find user by email or username
            User? user = await _userRepository.GetUserByEmailOrUsernameAsync(emailOrUsername); // FIXED: Await the async call
            Console.WriteLine(user.Id);
            // 2. Check if user exists
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            // 3. Verify password
            if (!VerifyPassword(password, user.Password))
            {
                throw new Exception("Incorrect password.");
            }

            // 4. Generate JWT token
            var token = _jwtTokenGenerator.GenerateJwtToken(user.Id, user.Role, user.Username, user.FirstName);

            return new LoginResponse(
                user.Id,
                user.Email,
                user.Username,
                token,
                user.Role
            );
        }

        // Helper method to hash passwords
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        // Helper method to verify passwords
        private bool VerifyPassword(string inputPassword, string storedHash)
        {
            string hashedInput = HashPassword(inputPassword);
            return hashedInput == storedHash;
        }
    }
}
