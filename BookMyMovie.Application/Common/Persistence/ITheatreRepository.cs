using BookMyMovie.Application.Services.Theatres.TheatreDTOS;
using BookMyMovie.Contracts.Theatre;
using BookMyMovie.Domain.Entities;

namespace BookMyMovie.Application.Common.Persistence;

public interface ITheatreRepository
{
    Task AddAsync(Theatre theatre);
    Task<List<Theatre>> GetAllAsync();
    Task<Theatre?> GetByIdAsync(Guid id);
    
    Task UpdateAsync(Theatre theatre);
    Task DeleteAsync(Theatre theatre);
    
}