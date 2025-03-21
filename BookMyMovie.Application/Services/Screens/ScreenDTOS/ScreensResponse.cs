namespace BookMyMovie.Application.Services.Screens.ScreenDTOS;

public record ScreensResponse(
    Guid Id,
    Guid TheatreId,
    string TheatreName,
    DateTime ModifiedAt,
    string SeatLayout,
    int TotalSeats
    );