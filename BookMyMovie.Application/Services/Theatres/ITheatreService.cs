using BookMyMovie.Application.Services.Theatres.TheatreDTOS;
using BookMyMovie.Domain.Entities;

namespace BookMyMovie.Application.Services.Theatres;

public interface ITheatreService
{     public Task<TheatreCreateResponse> TheatreCreateAsync(
        string name,
        string location,
        string address
    );

    public Task<TheatreByIdResponse> GetTheatreByIdAsync(Guid Id);
    public Task<List<Theatre>> GetAllTheatresAsync();
    
    public Task DeleteTheatreAsync(Guid Id);
    public Task UpdateTheatreAsync(Guid Id,TheatreUpdateRequest theatre);
    


}