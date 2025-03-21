using BookMyMovie.Application.Services.Booking.BookingDTOs;
using BookMyMovie.Domain.Entities;

namespace BookMyMovie.Application.Services.Booking;

public interface IBookingService
{
    public Task CreateBooking(
        Guid UserId,
        Guid ShowTimeId,
        int Count,
        string Seats,
        double amount
        );
    public Task UpdateBooking(Guid Id, string Seats);
    public Task DeleteBooking(Guid bookingId);
    public Task<List<BookingDTO>> GetAllBookings(Guid id);
    public Task<BookingDTO> GetBookingById(Guid Id);
    public Task<List<BookingDTO>> GetAllBookingsByUserId(Guid UserId);
    public Task<List<BookingDTO>> GetAllBookingsByShowId(Guid ShowId);
}

public class CreateBoooking
{
}