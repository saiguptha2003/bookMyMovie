namespace BookMyMovie.Contracts.Theatre;

public record TheatreAddResponse
(
    Guid Id,
    string Name,
    string Location
    );