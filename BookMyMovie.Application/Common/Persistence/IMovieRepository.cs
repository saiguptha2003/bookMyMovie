using BookMyMovie.Contracts;
using BookMyMovie.Domain.Entities;

namespace BookMyMovie.Application.Common.Persistence;

public interface IMovieRepository
{
    Task AddAsync(Movie? movie);
    Task DeleteAsync(Guid Id);
    Task<List<Movie?>> GetAllMoviesAsync(MoviesQuery query);
    Task<Movie?> GetMovieById(Guid Id);
    Task UpdateMovie(Movie movie);
    Task<List<Movie?>> GetMoviesByGenre(string genre);
    Task <Movie> GetMovieByTitle(string title);
    
    Task<int> IncrementLikeCountAsync(Guid Id);
    Task<double> ModifyRatingAsync(Guid Id, float rating);
    
}