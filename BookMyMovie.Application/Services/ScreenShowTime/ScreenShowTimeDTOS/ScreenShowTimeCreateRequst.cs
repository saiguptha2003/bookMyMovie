namespace BookMyMovie.Application.Services.ScreenShowTime.ScreenShowTimeDTOS;

public record ScreenShowTimeCreateRequst(
    Guid ScreenId,
    Guid MovieId,
    string BookedSeats,
    DateOnly ShowDate,
    TimeOnly ShowTime
    );