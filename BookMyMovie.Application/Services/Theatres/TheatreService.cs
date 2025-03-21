using BookMyMovie.Application.Common.Persistence;
using BookMyMovie.Application.Services.Theatres.TheatreDTOS;
using BookMyMovie.Contracts.Theatre;
using BookMyMovie.Domain.Entities;

namespace BookMyMovie.Application.Services.Theatres;

public class TheatreService: ITheatreService
{
    private readonly ITheatreRepository _theatreRepository;

    public TheatreService(ITheatreRepository theatreRepository)
    {
        _theatreRepository = theatreRepository;
    }
    public async Task<TheatreCreateResponse> TheatreCreateAsync(string name, string location, string address)
    {
        var theatreId= Guid.NewGuid();
        var theatre = new Theatre
        {
            Id = theatreId,
            Name = name,
            Location = location,
            Address = address,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow

        };
        Console.WriteLine("Adding theatre...");
        await _theatreRepository.AddAsync(theatre); // FIXED: Await the async method
        Console.WriteLine($"theatre registered: {theatre.Id}, ID: {theatre.Name}");
        
        var response = new TheatreCreateResponse(
            theatre.Name,
            theatre.Location,
            theatre.Id
            );
        return response;
        
    }

    public async Task<TheatreByIdResponse> GetTheatreByIdAsync(Guid Id)
    {
        var theatre = await _theatreRepository.GetByIdAsync(Id);
        return new TheatreByIdResponse(
            theatre.Id,
            theatre.Name,
            theatre.Location,
            theatre.Address,
            theatre.CreatedAt,
            theatre.UpdatedAt
            );
    }

    public async Task<List<Theatre>> GetAllTheatresAsync()
    {
        var theatres = await _theatreRepository.GetAllAsync();
        return theatres;
    }

    public async Task DeleteTheatreAsync(Guid id)
    {
        var theatre = await _theatreRepository.GetByIdAsync(id);
        if (theatre != null)
        {
            await _theatreRepository.DeleteAsync(theatre);
        }
    }

    public async Task UpdateTheatreAsync( Guid id,TheatreUpdateRequest theatre)
    {
        var existingTheatre = await _theatreRepository.GetByIdAsync(id);
        if (existingTheatre == null)
        {
            throw new KeyNotFoundException("Theatre not found.");
        }

        existingTheatre.Name = theatre.Name;
        existingTheatre.Location = theatre.Location;
        existingTheatre.Address = theatre.Address;
        existingTheatre.UpdatedAt = DateTime.UtcNow;
        await _theatreRepository.UpdateAsync(existingTheatre);
    }
}