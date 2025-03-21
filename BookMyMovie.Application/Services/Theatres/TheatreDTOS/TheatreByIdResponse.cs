namespace BookMyMovie.Application.Services.Theatres.TheatreDTOS;

public record TheatreByIdResponse
(
    Guid Id,
    string Name,
    string Location,
    string Address,
    DateTime CreatedAt,
    DateTime UpdatedAt
    );