namespace BookMyMovie.Contracts.Screen;

public record ScreenCreateRequest
(
    Guid TheatreId,
    string SeatLayout,
    int TotalSeats
);