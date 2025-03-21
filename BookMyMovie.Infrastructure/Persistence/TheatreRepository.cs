using BookMyMovie.Application.Common.Persistence;
using BookMyMovie.Domain.Entities;
using BookMyMovie.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace BookMyMovie.Infrastructure.Persistence;

public class TheatreRepository : ITheatreRepository
{
    private readonly BookMyMovieContext _context;

    public TheatreRepository(BookMyMovieContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Theatre theatre)
    {
        // Check if a theatre with the same name already exists
        var existingTheatre = await _context.Theatres
            .FirstOrDefaultAsync(t => t.Name == theatre.Name);

        if (existingTheatre != null)
        {
            throw new Exception("theatre with this name already exists.");
        }

        await _context.Theatres.AddAsync(theatre);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Theatre>> GetAllAsync()
    {
        return await _context.Theatres.ToListAsync();
    }

    public async Task<Theatre?> GetByIdAsync(Guid id)
    {
        return await _context.Theatres.FindAsync(id);
    }

    public async Task UpdateAsync(Theatre theatre)
    {
        var existingTheatre = await _context.Theatres.FindAsync(theatre.Id);
        if (existingTheatre == null)
        {
            throw new Exception("theatre not exists.");
        }

        existingTheatre.Name = theatre.Name;
        existingTheatre.Location = theatre.Location;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Theatre theatre)
    {
        var existingTheatre = await _context.Theatres.FindAsync(theatre.Id);
        if (existingTheatre == null)
        {
            throw new Exception("theatre deleted");
        }
        _context.Theatres.Remove(existingTheatre);
        await _context.SaveChangesAsync();
    }
}
