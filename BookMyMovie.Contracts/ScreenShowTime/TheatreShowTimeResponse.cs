
namespace  BookMyMovie.Contracts.ScreenShowTime
{
    
}
public record TheatreShowTimeResponse(
    Guid TheatreId,
    List<ShowTimeDto> ShowTimes
);

public record ShowTimeDto(
    TimeOnly ShowTime,
    DateOnly ShowDate,
    DateTime UpdatedAt,
    Guid ScreenId,
    string BookedSeats
);