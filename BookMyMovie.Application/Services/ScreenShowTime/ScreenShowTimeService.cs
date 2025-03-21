using BookMyMovie.Application.Services.ScreenShowTime.ScreenShowTimeDTOS;
using BookMyMovie.Application.Common.Persistence;
using BookMyMovie.Contracts.ScreenShowTime;
using BookMyMovie.Domain.Entities;

namespace BookMyMovie.Application.Services.ScreenShowTime;

public class ScreenShowTimeService : IScreenShowTimeService
{
    private readonly IShowTimeRepository _screenShowTimeRepository;
    private readonly IMovieRepository _movieRepository;
    private readonly ITheatreRepository _theatreRepository;

    public ScreenShowTimeService(
        IShowTimeRepository showTimeRepository,
        IMovieRepository movieRepository,
        ITheatreRepository theatreRepository)
    {
        _screenShowTimeRepository = showTimeRepository;
        _movieRepository = movieRepository;
        _theatreRepository = theatreRepository;
    }
    public async Task  CreateShowTime(ScreenShowTimeCreateRequst request)
    {
        var showTime = new Domain.Entities.ScreenShowTime
        {
            Id = Guid.NewGuid(),
            BookedSeats = request.BookedSeats,
            ScreenId = request.ScreenId,
            MovieId = request.MovieId,
            ShowDate = request.ShowDate,
            ShowTime = request.ShowTime,
            UpdatedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };
       await _screenShowTimeRepository.AddAsync(showTime);
    }

    public async Task<List<ShowTimeResponse>> GetShowTimes(ShowQuery query)
    {
        var showTimes = await _screenShowTimeRepository.ScreenShowTimes(query);

        var result = showTimes
            .GroupBy(s => new { 
                TheatreId = s.Screen?.TheatreId ?? Guid.Empty,           // Null-safe navigation// Null-safe navigation
            })
            .Select(g => new ShowTimeResponse(
                g.Key.TheatreId,
                g.Select(s => new ShowTimeDTO(
                    s.Id,
                    s.Movie?.Name ?? "Unknown",                        // Null-safe access
                    s.Movie?.Genre ?? "Unknown",                       // Null-safe access
                    s.ShowTime,
                    s.ShowDate,
                    s.UpdatedAt,
                    s.ScreenId,
                    s.BookedSeats
                )).ToList()
            ))
            .ToList();

        return result;

    }

    public async Task<string> UpdateBookedSeats(Guid showId, string bookedSeats)
    {
        var showTime = await _screenShowTimeRepository.GetShowTimeByID(showId);
        if (showTime == null)
        {
            throw new ArgumentException("Showtime not found.");
        }

        await _screenShowTimeRepository.UpdateBookedSeats(showId, bookedSeats);

        return showTime.BookedSeats;
    }

    public async Task<string> GetBookedSeats(Guid showId)
    {
        var showTime = await _screenShowTimeRepository.GetShowTimeByID(showId);
        if (showTime == null)
        {
            throw new ArgumentException("Showtime not found.");
        }

        return showTime.BookedSeats;
    }

    public async Task<DateOnly> UpdateShowDate(Guid showId, DateOnly showDate)
    {
        var showTime = await _screenShowTimeRepository.GetShowTimeByID(showId);
        if (showTime == null)
        {
            throw new ArgumentException("Showtime not found.");
        }

        showTime.ShowDate = showDate;
        showTime.UpdatedAt = DateTime.UtcNow;

        await _screenShowTimeRepository.UpdateAsync(showTime);

        return showTime.ShowDate;
    }

    public async Task<TimeOnly> UpdateShowTime(Guid showId, TimeOnly showTime)
    {
        var show = await _screenShowTimeRepository.GetShowTimeByID(showId);
        if (show == null)
        {
            throw new ArgumentException("Showtime not found.");
        }

        show.ShowTime = showTime;
        show.UpdatedAt = DateTime.UtcNow;

        await _screenShowTimeRepository.UpdateAsync(show);

        return show.ShowTime;
    }

    public async Task<Movie> GetMovieByShowTime(Guid showId)
    {
        var showTime = await _screenShowTimeRepository.GetShowTimeByID(showId);
        if (showTime == null)
        {
            throw new ArgumentException("Showtime not found.");
        }

        var movie = await _movieRepository.GetMovieById(showTime.MovieId);
        if (movie == null)
        {
            throw new ArgumentException("Movie not found.");
        }

        return movie;
    }

    public async Task<ShowTimeDTO> GetShowId(Guid showId)
    {
        var showTime = await _screenShowTimeRepository.GetShowTimeByID(showId);
        if (showTime == null)
        {
            throw new ArgumentException("Showtime not found.");
        }

        return new ShowTimeDTO(
            showTime.Id,
            showTime.Movie?.Name ?? "Unknown", // Null-safe access
            showTime.Movie?.Genre ?? "Unknown", // Null-safe access
            showTime.ShowTime,
            showTime.ShowDate,
            showTime.UpdatedAt,
            showTime.ScreenId,
            showTime.BookedSeats
        );
    }
    //
    // public async Task<List<ShowTimeResponse>>  GetShowTimesBYMovieIdAndLocation(Guid moviId, string Location)
    // {
    //     var movie = await _movieRepository.GetMovieById(moviId);
    //     if (movie == null)
    //     {
    //         throw new ArgumentException("Movie not found.");
    //     }
    //     var showTimes = await _screenShowTimeRepository.ShowTimeByMovieIdAndLocation(moviId, Location);
    //     return showTimes
    //         .GroupBy(s => s.Screen.TheatreId)
    //         .Select(g => new ShowTimeResponse(
    //             g.Key,
    //             g.Select(s => new ShowTimeDTO
    //             ( 
    //                 s.Id,
    //                 s.ShowTime,
    //                 s.ShowDate,
    //                 s.UpdatedAt,
    //                 s.ScreenId,
    //                 s.BookedSeats
    //             )).ToList()
    //         ))
    //         .ToList();
    // }
}