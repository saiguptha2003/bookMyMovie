using BookMyMovie.Application.Services.Screens.ScreenDTOS;
using BookMyMovie.Domain.Entities;

namespace BookMyMovie.Application.Services.Screens;

public interface IScreenService
{
    public Task<ScreenCreateResponse> ScreenCreateAsync(
        Guid theatreId,
        int totalSeats,
        string seatLayout
        );
    public Task<IEnumerable<ScreensResponse>> GetAllScreensAsync();
    
    public Task<ScreensResponse> GetScreenByIdAsync(Guid Id);
    
    public Task<IEnumerable<ScreensResponse>> GetScreensByTheatreIdAsync(Guid TheatreId);
    
    public Task<int> GetTotalNumberOfSeatsAsync(Guid Id);
    public Task<string> GetSeatLayoutByIdAsync(Guid Id);
    
    public Task<ScreensResponse> GetScreenByTheatreNameAsync(string theatreName);
    
    public Task DeleteScreen(Guid id);
    public  Task<string> UpdateScreenLayout(Guid id, string seatLayout);
    
    public Task<int> UpdateTotalSeats(Guid id, int totalSeats);
    
    
    
    
    
}