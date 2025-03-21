namespace BookMyMovie.Application.Services.Movies.MovieDTOS;

public record ListMoviesResponse(
    Guid Id,
    string Name,
    string Genre,
    string Cast,
    int Likes,
    double Rating ,
    string Description,
    string PosterUrl,
    DateTime ReleaseDate,
    DateTime Created,
    DateTime Updated
    
    );