using BookMyMovie.Application.Common.Persistence;
using BookMyMovie.Application.Services.Booking.BookingDTOs;
using BookMyMovie.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMyMovie.Application.Services.Booking;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IShowTimeRepository _showTimeRepository; // Add this repository

    public BookingService(
        IBookingRepository bookingRepository,
        IShowTimeRepository showTimeRepository) // Inject the new repository
    {
        _bookingRepository = bookingRepository;
        _showTimeRepository = showTimeRepository;
    }

    // Add this method to validate ShowTimeId


    public async Task CreateBooking(Guid UserId, Guid ShowTimeId, int Count, string Seats, double amount)
    {
        // First validate that the ShowTimeId exists
        var booking = new Domain.Entities.Booking
        {
            Id = Guid.NewGuid(),
            UserId = UserId,
            ShowTimeId = ShowTimeId,
            Seats = Seats,
            Count = Count,
            BookingDate = DateTime.UtcNow,
            BookingTime = DateTime.UtcNow,
            TotalAmount = amount,
            SnacksOrder = Guid.Empty // Added this to match your schema
        };
        
        await _bookingRepository.AddAsync(booking);
    }

    // Rest of your methods remain the same
    public async Task UpdateBooking(Guid Id, string Seats)
    {
        var booking = await _bookingRepository.GetByIdAsync(Id);
        if (booking == null)
        {
            throw new ArgumentException("Booking not found.");
        }
        booking.Seats = Seats;
        booking.BookingTime = DateTime.UtcNow;
        await _bookingRepository.UpdateAsync(booking);
    }

    public async Task DeleteBooking(Guid bookingId)
    {
        var booking = await _bookingRepository.GetByIdAsync(bookingId);
        if (booking == null)
        {
            throw new ArgumentException("Booking not found.");
        }
        await _bookingRepository.DeleteAsync(booking);
    }

    public async Task<List<BookingDTO>> GetAllBookings(Guid Id)
    {
        var bookings = await _bookingRepository.BookingListAsync(Id);
        return bookings.Select(b => new BookingDTO
        (
            b.Id,
            b.UserId,
            b.ShowTime.Movie.Name,
            b.TotalAmount,
            b.ShowTime.Screen.Theatre.Name,
            b.ShowTime.Screen.Theatre.Location,
            b.BookingDate,
            b.ShowTime.ShowDate,
            b.ShowTime.ShowTime,
            b.Seats,
            b.Count
        )).ToList();
    }

    public async Task<BookingDTO> GetBookingById(Guid Id)
    {
        var booking = await _bookingRepository.GetByIdAsync(Id);
        if (booking == null)
        {
            throw new ArgumentException("Booking not found.");
        }
        return new BookingDTO
        (
            booking.Id,
            booking.UserId,
            booking.ShowTime.Movie.Name,
            booking.TotalAmount,
            booking.ShowTime.Screen.Theatre.Name,
            booking.ShowTime.Screen.Theatre.Location,
            booking.BookingDate,
            booking.ShowTime.ShowDate,
            booking.ShowTime.ShowTime,
            booking.Seats,
            booking.Count
        );
    }

    public async Task<List<BookingDTO>> GetAllBookingsByUserId(Guid UserId)
    {
        var bookings = await _bookingRepository.BookingListByUserId(UserId);
        return bookings.Select(b => new BookingDTO
        (
            b.Id,
            b.UserId,
            b.ShowTime.Movie.Name,
            b.TotalAmount,
            b.ShowTime.Screen.Theatre.Name,
            b.ShowTime.Screen.Theatre.Location,
            b.BookingDate,
            b.ShowTime.ShowDate,
            b.ShowTime.ShowTime,
            b.Seats,
            b.Count
        )).ToList();
    }

    public async Task<List<BookingDTO>> GetAllBookingsByShowId(Guid ShowId)
    {
        var bookings = await _bookingRepository.BookingListByShowId(ShowId);
        return bookings.Select(b => new BookingDTO
        (
            b.Id,
            b.UserId,
            b.ShowTime.Movie.Name,
            b.TotalAmount,
            b.ShowTime.Screen.Theatre.Name,
            b.ShowTime.Screen.Theatre.Location,
            b.BookingDate,
            b.ShowTime.ShowDate,
            b.ShowTime.ShowTime,
            b.Seats,
            b.Count
        )).ToList();
    }
}