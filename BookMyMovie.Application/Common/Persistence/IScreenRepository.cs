using BookMyMovie.Application.Services.Theatres.TheatreDTOS;
using BookMyMovie.Contracts.Theatre;
using BookMyMovie.Domain.Entities;

namespace BookMyMovie.Application.Common.Persistence;

public interface IScreenRepository
{
    Task AddAsync(Screen screen);
    Task<List<Screen>> GetAllAsync();
    Task<Screen?> GetByIdAsync(Guid Id);
    
    Task UpdateAsync(Screen screen);
    Task DeleteAsync(Screen screen);
    public Task<List<Screen>> GetAllScreensByTheatreIdAsync(Guid theatreId);
    Task<string> GetScreenSeatLayoutAsync(Guid Id);
    Task<List<ScreenShowTime>> GetScreenShowTimesByIdAsync(Guid Id);
    Task<int> GetScreenTotalSeatsAsync(Guid Id);
    
    Task<List<Screen>> GetScreensByTheatreNameAsync(string theatreName);

}