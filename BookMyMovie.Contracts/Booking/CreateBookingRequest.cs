namespace BookMyMovie.Contracts.Booking;

public record CreateBookingRequest(
    Guid ShowTimeId,
    int Count,
    string Seats,
    double Amount
    
    );