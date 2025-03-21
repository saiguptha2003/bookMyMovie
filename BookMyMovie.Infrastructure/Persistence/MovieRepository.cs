using BookMyMovie.Application.Common.Persistence;
using BookMyMovie.Contracts;
using BookMyMovie.Domain.Entities;
using BookMyMovie.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace BookMyMovie.Infrastructure.Persistence;

public class MovieRepository : IMovieRepository
{
    private readonly BookMyMovieContext _movieRepository;
    private readonly BookMyMovieContext _showTimeRepository;
    

    public MovieRepository(BookMyMovieContext context)
    {
        _movieRepository = context;
        _showTimeRepository = context;

    }

    public async Task AddAsync(Movie? movie)
    {
        var existingMovie = await _movieRepository.Movies.AnyAsync(m => movie != null && m != null && m.Name == movie.Name);
        if (existingMovie)
        {
            throw new InvalidOperationException("A movie with the same title already exists.");
        }

        await _movieRepository.Movies.AddAsync(movie);
        await _movieRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var movie = await _movieRepository.Movies.FindAsync(id);
        if (movie == null)
        {
            throw new KeyNotFoundException("Movie not found.");
        }

        _movieRepository.Movies.Remove(movie);
        await _movieRepository.SaveChangesAsync();
    }

    public async Task<List<Movie?>> GetAllMoviesAsync(MoviesQuery query)
    {
            var movies = _movieRepository.Movies
                .Include(m => m.ShowTimes)  // Include related ShowTimes
                .ThenInclude(st => st.Screen)      // Include Screens
                .ThenInclude(sc => sc.Theatre)     // Include Theatre for location filtering
                .AsQueryable();

            // Apply filters
            if (!string.IsNullOrWhiteSpace(query.Genre))
            {
                movies = movies.Where(m => m.Genre == query.Genre);
            }

            if (!string.IsNullOrWhiteSpace(query.MovieName))
            {
                movies = movies.Where(m => m.Name == query.MovieName);
            }

            // Filter by location using Theatre from ShowTimes
            if (!string.IsNullOrWhiteSpace(query.Location))
            {
                movies = movies
                    .Where(m => m.ShowTimes
                        .Any(st => st.Screen.Theatre.Location == query.Location));
            }

            return await movies.ToListAsync();
        }

    public async Task<Movie?> GetMovieById(Guid id)
    {
        return await _movieRepository.Movies.FindAsync(id);
    }

    public async Task UpdateMovie(Movie movie)
    {
        var existingMovie = await _movieRepository.Movies.FindAsync(movie.Id);
        if (existingMovie == null)
        {
            throw new KeyNotFoundException("Movie not found.");
        }

        existingMovie.Name = movie.Name;
        existingMovie.Genre = movie.Genre;
        existingMovie.Description = movie.Description;
        existingMovie.ReleaseDate = movie.ReleaseDate;
        existingMovie.PosterUrl= movie.PosterUrl;
        existingMovie.ReleaseDate= movie.ReleaseDate;
        existingMovie.Rating = movie.Rating;
        existingMovie.Cast = movie.Cast;

        _movieRepository.Movies.Update(existingMovie);
        await _movieRepository.SaveChangesAsync();
    }

    public async Task<List<Movie?>> GetMoviesByGenre(string genre)
    {
        return await _movieRepository.Movies
            .Where(m => m.Genre.ToLower() == genre.ToLower())
            .ToListAsync();
    }

    public async Task<Movie> GetMovieByTitle(string title)
    {
        return await _movieRepository.Movies
            .FirstOrDefaultAsync(m => m.Name.ToLower() == title.ToLower());
    }

    public async Task<int> IncrementLikeCountAsync(Guid id)
    {
        var movie = await _movieRepository.Movies.FindAsync(id);
        if (movie == null)
        {
            throw new KeyNotFoundException("Movie not found.");
        }

        movie.Likes += 1;
        _movieRepository.Movies.Update(movie);
        await _movieRepository.SaveChangesAsync();
        return movie.Likes;
    }

    public async Task<double> ModifyRatingAsync(Guid id, float rating)
    {
        var movie = await _movieRepository.Movies.FindAsync(id);
        if (movie == null)
        {
            throw new KeyNotFoundException("Movie not found.");
        }

        if (rating < 0 || rating > 10)
        {
            throw new ArgumentException("Rating must be between 0 and 10.");
        }
        movie.Rating = (movie.Rating * movie.Rating + rating) / (movie.Rating + 1);
        movie.Rating += 1;

        _movieRepository.Movies.Update(movie);
        await _movieRepository.SaveChangesAsync();
        return movie.Rating;
    }

}
