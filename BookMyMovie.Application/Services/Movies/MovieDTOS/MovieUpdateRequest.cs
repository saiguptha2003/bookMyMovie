namespace BookMyMovie.Application.Services.Movies.MovieDTOS;

public record MovieUpdateRequest(
    string Name,
    string Description,
    string Cast,
    string Genre,
    DateTime ReleaseDate,
    double Rating,
    string PosterUrl
    );