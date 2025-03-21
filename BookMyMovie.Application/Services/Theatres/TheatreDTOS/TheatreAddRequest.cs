namespace BookMyMovie.Application.Services.Theatres.TheatreDTOS;

public record TheatreUpdateRequest(
    string Name,
    string Location,
    string Address
);