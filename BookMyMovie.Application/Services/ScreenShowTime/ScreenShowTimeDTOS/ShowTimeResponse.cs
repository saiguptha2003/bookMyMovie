namespace BookMyMovie.Application.Services.ScreenShowTime.ScreenShowTimeDTOS;

public record ShowTimeDTO
(
    Guid Id,
    string MovieName,
    string Genre,
    TimeOnly ShowTime,
    DateOnly ShowDate,
    DateTime UpdatedAt,
    Guid ScreenId,
    string BookedSeats
    
    
    );

public record ShowTimeResponse
    (
 Guid TheatreId,
 List<ShowTimeDTO> ShowTimes
);