namespace BookMyMovie.Contracts.Theatre;

public record TheatreAddRequest(
    string Name,
    string Location,
    string Address
    );