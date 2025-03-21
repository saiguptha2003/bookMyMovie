using System.Runtime.InteropServices.JavaScript;

namespace BookMyMovie.Application.Services.Movies.MovieDTOS;

public record MovieAddResponse(
    Guid Id,
    string Name,
    string Description,
    double Rating,
    DateTime Created,
    string genre,
    string cast,
    DateTime ReleaseDate
    );