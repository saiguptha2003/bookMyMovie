using BookMyMovie.Application.Common.Persistence;
using BookMyMovie.Application.Services.Movies.MovieDTOS;
using BookMyMovie.Application.Services.Theatres.TheatreDTOS;
using BookMyMovie.Contracts;
using BookMyMovie.Domain.Entities;

namespace BookMyMovie.Application.Services.Movies;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;

    public MovieService(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<MovieAddResponse> MovieCreateAsync(string name, string description, DateTime releaseDate, string posterUrl, string genre, string cast)
    {
        // Check if movie already exists
        var existingMovie = await _movieRepository.GetMovieByTitle(name);
        if (existingMovie != null)
        {
            throw new InvalidOperationException("A movie with the same title already exists.");
        }

        var movie = new Movie
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            ReleaseDate = releaseDate,
            PosterUrl = posterUrl,
            Genre = genre,
            Cast = cast,
            Likes = 0,
            Rating = 0,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _movieRepository.AddAsync(movie);

        return new MovieAddResponse(
            movie.Id,
            movie.Name,
            movie.Description,
            movie.Rating,
            movie.CreatedAt,
            movie.Genre,
            movie.Cast,
            movie.ReleaseDate
            );
    }

    public async Task<MovieResponse> GetMovieByIdAsync(Guid Id)
    {
        var movie = await _movieRepository.GetMovieById(Id);
        if (movie == null)
        {
            throw new KeyNotFoundException("Movie not found.");
        }

        return new MovieResponse(
            movie.Id, 
            movie.Name, 
            movie.Genre, movie.Cast, movie.Likes, movie.Rating, movie.Description, movie.PosterUrl,movie.ReleaseDate, movie.CreatedAt, movie.UpdatedAt);
    }

    public async Task<List<ListMoviesResponse?>> GetAllMoviesAsync(MoviesQuery query)
    {
        var movies= await _movieRepository.GetAllMoviesAsync(query);
        var response=new List<ListMoviesResponse?>();
        foreach (var movie in movies)
        {
            response.Add(new ListMoviesResponse(
                movie.Id,
                movie.Name,
                movie.Genre,
                movie.Cast,
                movie.Likes,
                movie.Rating,
                movie.Description,
                movie.PosterUrl,
                movie.ReleaseDate,
                movie.CreatedAt,
                movie.UpdatedAt
                ));
            
        }
        return response;
    }

    public async Task DeleteMovieByIdAsync(Guid Id)
    {
        var movie = await _movieRepository.GetMovieById(Id);
        if (movie == null)
        {
            throw new KeyNotFoundException("Movie not found.");
        }

        await _movieRepository.DeleteAsync(Id);
    }
    
    public async Task UpdateMovieAsync(Guid Id, MovieUpdateRequest movieUpdateRequest)
    {
        var movie = await _movieRepository.GetMovieById(Id);
        if (movie == null)
        {
            throw new KeyNotFoundException("Movie not found.");
        }

        movie.Name = movieUpdateRequest.Name ?? movie.Name;
        movie.Description = movieUpdateRequest.Description ?? movie.Description;
        movie.Cast=movieUpdateRequest.Cast ?? movie.Cast;
        movie.Genre=movieUpdateRequest.Genre ?? movie.Genre;
        movie.Rating = movieUpdateRequest.Rating;
        movie.PosterUrl=movieUpdateRequest.PosterUrl ?? movie.PosterUrl;
        movie.ReleaseDate = movieUpdateRequest.ReleaseDate;
        movie.UpdatedAt=DateTime.UtcNow;
        

        await _movieRepository.UpdateMovie(movie);
    }

    public async Task<int> IncrementLikeCountAsync(Guid id)
    {
        var movie = await _movieRepository.GetMovieById(id);
        if (movie == null)
        {
            throw new KeyNotFoundException("Movie not found.");
        }

        await _movieRepository.IncrementLikeCountAsync(id);
        return movie.Likes + 1;
    }

    public async Task<double> ModifyRatingAsync(Guid id, float rating)
    {
        var movie = await _movieRepository.GetMovieById(id);
        if (movie == null)
        {
            throw new KeyNotFoundException("Movie not found.");
        }

        await _movieRepository.ModifyRatingAsync(id, rating);
        return movie.Rating;
    }
}
