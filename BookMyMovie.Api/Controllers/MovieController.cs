using BookMyMovie.Application.Services.Movies;
using BookMyMovie.Application.Services.Movies.MovieDTOS;
using BookMyMovie.Contracts;
using BookMyMovie.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookMyMovie.Api.Controllers;

[Route("api")]
[ApiController]
public class MovieController : ControllerBase
{
    private readonly IMovieService _movieService;

    public MovieController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("admin/movies")]
    public async Task<IActionResult> CreateMovie([FromBody] MovieCreateRequest? request)
    {
        if (request == null)
        {
            return BadRequest("Invalid movie data.");
        }

        var response = await _movieService.MovieCreateAsync(
            request.Name, request.Description, request.ReleaseDate, 
            request.PosterUrl, request.Genre, request.Cast);

        return CreatedAtAction(nameof(GetMovieById), new { id = response.Id }, response);
    }

    [HttpGet("movies/{id}")]
    public async Task<MovieResponse> GetMovieById(Guid id, MoviesQuery request)
    {
            var movie = await _movieService.GetMovieByIdAsync(id);
            return movie;
            
     
    }
    [HttpGet("movies")]
    public async Task<IActionResult> GetAllMovies([FromQuery] MoviesQuery request)
    {
        var movies = await _movieService.GetAllMoviesAsync(request);
    
        if (movies == null || !movies.Any())
        {
            return NotFound("No movies found for the given criteria.");
        }

        return Ok(movies);
    }
    [Authorize(Roles = "Admin")]
    [HttpPut("admin/movies/{id}")]
    public async Task<IActionResult> UpdateMovie(Guid id, [FromBody] MovieUpdateRequest request)
    {
        try
        {
            await _movieService.UpdateMovieAsync(id, request);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    [Authorize(Roles = "Admin")]
    [HttpDelete("admin/movies/{id}")]
    public async Task<IActionResult> DeleteMovie(Guid id)
    {
        try
        {
            await _movieService.DeleteMovieByIdAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    // Increment movie like count
    [HttpPatch("movies/{id}/like")]
    public async Task<IActionResult> IncrementLike(Guid id)
    {
        try
        {
            var newLikeCount = await _movieService.IncrementLikeCountAsync(id);
            return Ok(new { Likes = newLikeCount });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPatch("movies/{id}/rating")]
    public async Task<IActionResult> ModifyRating(Guid id, [FromQuery] float rating)
    {
        try
        {
            var newRating = await _movieService.ModifyRatingAsync(id, rating);
            return Ok(new { Rating = newRating });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    

}
