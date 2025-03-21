using BookMyMovie.Application.Common.Persistence;
using BookMyMovie.Domain.Entities;
using BookMyMovie.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace BookMyMovie.Infrastructure.Persistence;

public class BookingRepository : IBookingRepository
{
    private readonly BookMyMovieContext _context;

    public BookingRepository(BookMyMovieContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Booking booking)
    {
        await _context.Bookings.AddAsync(booking);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Booking>> BookingListByUserId(Guid BId)
    {
         return await _context .Bookings
            .Where(b => b.UserId == BId)
            .Include(b => b.ShowTime)
            .ThenInclude(st => st.Movie)
            .Include(b => b.ShowTime)
            .ThenInclude(st => st.Screen)
            .ThenInclude(s => s.Theatre)
            .ToListAsync();
    }

    public async  Task<List<Booking>> BookingListByShowId(Guid SId)
    {
        return await _context.Bookings.Where(s => s.ShowTimeId == SId).ToListAsync();
    }

    public async Task<Booking> GetByIdAsync(Guid BId)
    {
        var booking = await _context.Bookings
            .Include(b => b.User)
            .Include(b => b.ShowTime)
            .FirstOrDefaultAsync(b => b.Id == BId);

        if (booking == null)
        {
            throw new ArgumentException("Booking not found.");
        }

        return booking;
    }

    public async Task UpdateAsync(Booking booking)
    {
        var existingBooking = await _context.Bookings.FindAsync(booking.Id);
        if (existingBooking == null)
        {
            throw new ArgumentException("Booking not found.");
        }

        _context.Entry(existingBooking).CurrentValues.SetValues(booking);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Booking booking)
    {
        var existingBooking = await _context.Bookings.FindAsync(booking.Id);
        if (existingBooking == null)
        {
            throw new ArgumentException("Booking not found.");
        }

        _context.Bookings.Remove(existingBooking);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Booking>> BookingListAsync(Guid userId)
    {
        var bookings = await _context.Bookings
            .Where(b => b.UserId == userId)
            .Include(b => b.ShowTime)
            .Include(b => b.User)
            .ToListAsync();

        return bookings;
    }
}
