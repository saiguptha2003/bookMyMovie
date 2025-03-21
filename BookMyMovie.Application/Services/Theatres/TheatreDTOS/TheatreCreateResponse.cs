namespace BookMyMovie.Application.Services.Theatres.TheatreDTOS;

public record TheatreCreateResponse
(
    string Name,
    string Location,
    Guid Id
);