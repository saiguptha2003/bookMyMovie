using BookMyMovie.Application.Common.Persistence;
using BookMyMovie.Application.Services.Screens.ScreenDTOS;
using BookMyMovie.Domain.Entities;

namespace BookMyMovie.Application.Services.Screens;

public class ScreenService : IScreenService
{
    private readonly IScreenRepository _screenRepository;
    private readonly ITheatreRepository _theatreRepository;

    public ScreenService(IScreenRepository screenRepository, ITheatreRepository theatreRepository)
    {
        _screenRepository = screenRepository;
        _theatreRepository = theatreRepository;
    }

    // Create a new screen
    public async Task<ScreenCreateResponse> ScreenCreateAsync(Guid theatreId, int totalSeats, string seatLayout)
    {
        var theatre = await _theatreRepository.GetByIdAsync(theatreId);
        if (theatre == null)
        {
            throw new ArgumentException("Theatre not found.");
        }
        var screen = new Screen
        {
            Id = Guid.NewGuid(),
            TheatreId = theatreId,
            TotalSeats = totalSeats,
            SeatLayout = seatLayout,
            CreatedAt = DateTime.UtcNow,
             ModifiedAt = DateTime.UtcNow
        };
        await _screenRepository.AddAsync(screen);

        return new ScreenCreateResponse
        (
            screen.Id,
            screen.TheatreId, 
            screen.ModifiedAt,
            screen.SeatLayout,
            screen.TotalSeats
        );
    }

    // Get all screens
    public async Task<IEnumerable<ScreensResponse>> GetAllScreensAsync()
    {
        var screens = await _screenRepository.GetAllAsync();

        return screens.Select(s => new ScreensResponse
        (
            s.Id,
            s.TheatreId,
            s.Theatre.Name,
            s.ModifiedAt,
            s.SeatLayout,
            s.TotalSeats
        ));
    }

    // Get screen by ID
    public async Task<ScreensResponse> GetScreenByIdAsync(Guid id)
    {
        var screen = await _screenRepository.GetByIdAsync(id);
        if (screen == null)
        {
            throw new ArgumentException("Screen not found.");
        }

        return new ScreensResponse
        (
            screen.Id,
            screen.TheatreId,
            screen.Theatre.Name,
            screen.ModifiedAt,
            screen.SeatLayout,
            screen.TotalSeats
        );
    }

    // Get all screens by theatre ID
    public async Task<IEnumerable<ScreensResponse>> GetScreensByTheatreIdAsync(Guid theatreId)
    {
        var screens = await _screenRepository.GetAllScreensByTheatreIdAsync(theatreId);

        return screens.Select(s => new ScreensResponse
        (
            s.Id,
            s.TheatreId,
            s.Theatre.Name,
            s.ModifiedAt,
            s.SeatLayout,
            s.TotalSeats
        ));
    }

    // Get the total number of seats for a screen
    public async Task<int> GetTotalNumberOfSeatsAsync(Guid id)
    {
        return await _screenRepository.GetScreenTotalSeatsAsync(id);
    }

    // Get seat layout by screen ID
    public async Task<string> GetSeatLayoutByIdAsync(Guid id)
    {
        return await _screenRepository.GetScreenSeatLayoutAsync(id);
    }

    // Get screens by theatre name
    public async Task<ScreensResponse> GetScreenByTheatreNameAsync(string theatreName)
    {
        var screens = await _screenRepository.GetScreensByTheatreNameAsync(theatreName);

        if (!screens.Any())
        {
            throw new ArgumentException("No screens found for the specified theatre.");
        }

        // Assuming returning the first screen if multiple are found
        var screen = screens.First();

        return new ScreensResponse
        (
            screen.Id,
            screen.TheatreId,
            screen.Theatre.Name,
            screen.ModifiedAt,
            screen.SeatLayout,
            screen.TotalSeats
        );
    }

    public async Task DeleteScreen(Guid id)
    {
        var screen= await _screenRepository.GetByIdAsync(id);
        if (screen != null) 
            await _screenRepository.DeleteAsync(screen);
    }


    public async Task<string> UpdateScreenLayout(Guid id, string seatLayout)
    {
        var screen= await _screenRepository.GetByIdAsync(id);
        screen.SeatLayout = seatLayout;
        await _screenRepository.UpdateAsync(screen);
        return screen.SeatLayout;
    }

    public async Task<int> UpdateTotalSeats(Guid id, int totalSeats)
    {
        var screen = await _screenRepository.GetByIdAsync(id);
        screen.TotalSeats = totalSeats;
        await _screenRepository.UpdateAsync(screen);
        return screen.TotalSeats;
    }
}
