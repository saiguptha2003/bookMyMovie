using BookMyMovie.Domain.Entities;

namespace BookMyMovie.Application.Common.Persistence;

public interface IUserRepository
{
    Task<User?> GetUserByEmailAsync(string email);
    Task<User?> GetUserByIdAsync(Guid userId);
    Task<User?> GetUserByUserNameAsync(string userName);
    Task<List<User>> GetUsersByRoleAsync(string role);
    Task<string?> GetUserLocationAsync(Guid userId);
    Task<string?> GetUserPhoneNumberAsync(Guid userId);
    Task<bool> UserExistsByPhoneNumberAsync(string phoneNumber);
    Task<User?> GetUserByEmailOrUsernameAsync(string emailOrUsername);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(User user);
}