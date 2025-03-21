namespace BookMyMovie.Application.Services.Screens.ScreenDTOS;

public record ScreenCreateResponse(
    Guid ScreenId,
    Guid TheatreId,
    DateTime ModifiedAt,
    string SeatLayout,
    int TotalSeats
    );