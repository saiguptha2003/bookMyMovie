using BookMyMovie.Application.Common.Persistence;
using BookMyMovie.Contracts.ScreenShowTime;
using BookMyMovie.Domain.Entities;
using BookMyMovie.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace BookMyMovie.Infrastructure.Persistence;

public class ShowTimeRepository : IShowTimeRepository
{
    private readonly BookMyMovieContext _context;

    public ShowTimeRepository(BookMyMovieContext context)
    {
        _context = context;
    }

    // Create a new showtime
    public async Task AddAsync(ScreenShowTime showTime)
    {

        await _context.ScreenShowTimes.AddAsync(showTime);
        await _context.SaveChangesAsync();
    }
    public async Task<ScreenShowTime?> GetShowTimeByID(Guid Id)
    {
        return await _context.ScreenShowTimes.FirstOrDefaultAsync(x => x.Id == Id);
    }
    // Get all showtimes
public async Task<List<ScreenShowTime>> ScreenShowTimes(ShowQuery query)
{   
    var queryable = _context.ScreenShowTimes
        .Include(st => st.Screen)
        .Include(st => st.Movie)
        .AsQueryable();

    if (!string.IsNullOrEmpty(query.Location))
        queryable = queryable.Where(st => st.Screen.Theatre.Location == query.Location);

    if (!string.IsNullOrEmpty(query.MovieName))
        queryable = queryable.Where(st => st.Movie.Name.Contains(query.MovieName));
    var showTimes = await queryable.ToListAsync();
    return showTimes;
}


    // Get showtimes by movie ID
    public async Task<List<ScreenShowTime>> ScreenShowTimesByMovieId(Guid movieId)
    {
        return await _context.ScreenShowTimes
            .Where(st => st.MovieId == movieId)
            .Include(st => st.Screen)
            .Include(st => st.Movie)
            .ToListAsync();
    }

    // Get showtimes by screen ID
    public async Task<ScreenShowTime?> ScreenShowTimesByScreenId(Guid screenId)
    {
        return await _context.ScreenShowTimes
            .Include(st => st.Screen)
            .Include(st => st.Movie)
            .FirstOrDefaultAsync(st => st.ScreenId == screenId);
    }

    // Get showtimes by theatre ID
    public async Task<List<ScreenShowTime>> ScreenShowTimeByTheatreId(Guid theatreId)
    {
        return await _context.ScreenShowTimes
            .Include(st => st.Screen)
            .ThenInclude(s => s.Theatre)
            .Where(st => st.Screen.Theatre.Id == theatreId)
            .ToListAsync();
    }
    
    public async Task<string> UpdateBookedSeats(Guid showId, string bookedSeats)
    {
        if (string.IsNullOrEmpty(bookedSeats))
        {
            return "Booked seats cannot be empty";
        }

        var showTime = await _context.ScreenShowTimes
            .FirstOrDefaultAsync(st => st.Id == showId);
    
        if (showTime == null)
        {
            return "Showtime not found";
        }
    
        // Handle case where BookedSeats might be null or empty
        if (string.IsNullOrEmpty(showTime.BookedSeats))
        {
            showTime.BookedSeats = bookedSeats;
        }
        else
        {
            showTime.BookedSeats = showTime.BookedSeats + "," + bookedSeats;
        }
    
        showTime.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return "Updated successfully";
    }

    // Get movie by screen ID
    public async Task<Movie?> GetMovieByScreenId(Guid id)
    {
        var showTime = await _context.ScreenShowTimes
            .Include(st => st.Movie)
            .FirstOrDefaultAsync(st => st.ScreenId == id);

        return showTime?.Movie;
    }

    // Get theatre by screen ID
    public async Task<Theatre?> GetTheatreByScreenId(Guid id)
    {
        var showTime = await _context.ScreenShowTimes
            .Include(st => st.Screen)
            .ThenInclude(s => s.Theatre)
            .FirstOrDefaultAsync(st => st.ScreenId == id);

        return showTime?.Screen?.Theatre;
    }
	public async Task UpdateAsync( ScreenShowTime showTime)
    {
        _context.ScreenShowTimes.Update(showTime); 
        await _context.SaveChangesAsync();  
    }
}
