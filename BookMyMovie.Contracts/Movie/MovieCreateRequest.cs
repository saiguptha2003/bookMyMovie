namespace BookMyMovie.Contracts;

public record MovieCreateRequest(
    string Name,
    string Description,
    DateTime ReleaseDate,
    string PosterUrl,
    string Genre,
    string Cast,
    double Rating,
    int Likes
    );