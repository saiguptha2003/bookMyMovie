using BookMyMovie.Application.Common.Persistence;
using BookMyMovie.Domain.Entities;
using BookMyMovie.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace BookMyMovie.Infrastructure.Persistence;

public class ScreenRepository : IScreenRepository
{
    private readonly BookMyMovieContext _context;

    public ScreenRepository(BookMyMovieContext context)
    {
        _context = context;
    }

    // Add a new screen
    public async Task AddAsync(Screen screen)
    {
        await _context.Screens.AddAsync(screen);
        await _context.SaveChangesAsync();
    }

    // Get all screens
    public async Task<List<Screen>> GetAllAsync()
    {
        return await _context.Screens
            .Include(s => s.Theatre)  // Include related Theatre
            .ToListAsync();
    }

    // Get screen by ID
    public async Task<Screen?> GetByIdAsync(Guid id)
    {
        return await _context.Screens
            .Include(s => s.Theatre)  // Include Theatre data
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    // Update a screen
    public async Task UpdateAsync(Screen screen)
    {
        _context.Screens.Update(screen);
        await _context.SaveChangesAsync();
    }

    // Delete a screen
    public async Task DeleteAsync(Screen screen)
    {
        _context.Screens.Remove(screen);
        await _context.SaveChangesAsync();
    }
    

    // Get all screens by theatre ID
    public async Task<List<Screen>> GetAllScreensByTheatreIdAsync(Guid theatreId)
    {
        return await _context.Screens
            .Where(s => s.TheatreId == theatreId)
            .Include(s => s.Theatre)
            .ToListAsync();
    }

    // Get screen seat layout as JSON or string
    public async Task<string> GetScreenSeatLayoutAsync(Guid id)
    {
        var screen = await _context.Screens
            .FirstOrDefaultAsync(s => s.Id == id);

        return screen?.SeatLayout ?? string.Empty;
    }

    // ✅ Get all showtimes for a specific screen
    public async Task<List<ScreenShowTime>> GetScreenShowTimesByIdAsync(Guid screenId)
    {
        return await _context.ScreenShowTimes
            .Where(st => st.ScreenId == screenId)  // Fixed the filtering
            .Include(st => st.Movie)               // Include Movie details
            .Include(st => st.Screen)              // Include Screen details
            .ToListAsync();
    }

    // Get the total number of seats for a screen
    public async Task<int> GetScreenTotalSeatsAsync(Guid id)
    {
        var screen = await _context.Screens
            .FirstOrDefaultAsync(s => s.Id == id);

        return screen?.TotalSeats ?? 0;
    }

    public async Task<List<Screen>> GetScreensByTheatreNameAsync(string theatreName)
    {
        return await _context.Screens
            .Include(s => s.Theatre)                      // Include Theatre details
            .Where(s => s.Theatre.Name == theatreName)    // Filter by theatre name
            .ToListAsync();
    }
}
