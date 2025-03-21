using BookMyMovie.Application.Services.Movies.MovieDTOS;
using BookMyMovie.Application.Services.Theatres.TheatreDTOS;
using BookMyMovie.Contracts;
using BookMyMovie.Domain.Entities;

namespace BookMyMovie.Application.Services.Movies;

public interface IMovieService
{     public Task<MovieAddResponse> MovieCreateAsync(
        string name,
        string description,
        DateTime releaseDate,
        string posterUrl,
        string genre,
        string cast
    );

    public Task<MovieResponse> GetMovieByIdAsync(Guid Id);
    public Task<List<ListMoviesResponse?>> GetAllMoviesAsync(MoviesQuery query);
    
    public Task DeleteMovieByIdAsync(Guid Id);
    public Task UpdateMovieAsync(Guid Id,MovieUpdateRequest movieUpdateRequest);
    public Task<int> IncrementLikeCountAsync(Guid Id);
    public Task<double> ModifyRatingAsync(Guid Id, float  rating);
    
    


}