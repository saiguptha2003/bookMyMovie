namespace BookMyMovie.Application.Services.Booking.BookingDTOs;

public record BookingDTO(
    Guid BookingId,
    Guid UserId,
    string MoviName,
    double TotalAmount,
    string TheatreName,
    string TheatreLocation,
    DateTime BookingDate,
    DateOnly ShowDate,
    TimeOnly ShowTime,
    string Seats,
    int Count
    );