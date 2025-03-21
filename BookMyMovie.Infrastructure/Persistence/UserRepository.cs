using BookMyMovie.Application.Common.Persistence;
using BookMyMovie.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookMyMovie.Infrastructure.Repository;

namespace BookMyMovie.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private readonly BookMyMovieContext _context;

    public UserRepository(BookMyMovieContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetUserByIdAsync(Guid userId)
    {
        return await _context.Users.FindAsync(userId);
    }

    public async Task<User?> GetUserByUserNameAsync(string userName)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == userName);
    }

    public async Task<List<User>> GetUsersByRoleAsync(string role)
    {
        return await _context.Users.Where(u => u.Role == role).ToListAsync();
    }

    public async Task<string?> GetUserLocationAsync(Guid userId)
    {
        return await _context.Users
            .Where(u => u.Id == userId)
            .Select(u => u.Location)
            .FirstOrDefaultAsync();
    }

    public async Task<string?> GetUserPhoneNumberAsync(Guid userId)
    {
        return await _context.Users
            .Where(u => u.Id == userId)
            .Select(u => u.PhoneNumber)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> UserExistsByPhoneNumberAsync(string phoneNumber)
    {
        return await _context.Users.AnyAsync(u => u.PhoneNumber == phoneNumber);
    }

    public async Task<User?> GetUserByEmailOrUsernameAsync(string emailOrUsername)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == emailOrUsername || u.Username == emailOrUsername);
    }

    public async Task AddAsync(User user)
    {
        if (await _context.Users.AnyAsync(u => u.Email == user.Email))
        {
            throw new Exception("User with this email already exists.");
        }

        if (await _context.Users.AnyAsync(u => u.Username == user.Username))
        {
            throw new Exception("User with this username already exists.");
        }

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        var existingUser = await _context.Users.FindAsync(user.Id);
        if (existingUser != null)
        {
            _context.Entry(existingUser).CurrentValues.SetValues(user);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
}

