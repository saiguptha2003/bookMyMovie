using BookMyMovie.Application.Services.Theatres;
using BookMyMovie.Application.Services.Theatres.TheatreDTOS;
using BookMyMovie.Contracts.Theatre;
using BookMyMovie.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BookMyMovie.Api.Controllers;

[ApiController]
[Route("api")]
public class TheatreController: ControllerBase
{
    private readonly ITheatreService _theatreService;

    public TheatreController(ITheatreService theatreService)
    {
        _theatreService = theatreService;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("admin/theatres")]
    public async Task<TheatreAddResponse> Add([FromBody] TheatreAddRequest theatreAddRequest)
    {
        var register = await _theatreService.TheatreCreateAsync(
            theatreAddRequest.Name,
            theatreAddRequest.Location,
            theatreAddRequest.Address
        );

        return new TheatreAddResponse(
            register.Id,
            register.Name,
            register.Location
        );

    }
    
    [HttpGet("theatres/{id}")]
    public async Task<TheatreByIdResponse> GetTheatre(Guid id)
    {
        var theatre = await _theatreService.GetTheatreByIdAsync(id);
    
        if (theatre == null)
        {
            throw new KeyNotFoundException("Theatre not found.");
        }

        return new TheatreByIdResponse(
            theatre.Id,
            theatre.Name,
            theatre.Location,
            theatre.Address,
            theatre.CreatedAt,
            theatre.UpdatedAt
        );
    }
    [HttpGet("theatres")]
    public async Task<ActionResult<IEnumerable<Theatre>>> GetTheatres()
    {
        var theatres = await _theatreService.GetAllTheatresAsync();
        return theatres;
    }
    [Authorize(Roles = "Admin")]
    [HttpDelete("admin/theatres/{id}")]
    public async Task<IActionResult> DeleteTheatre(Guid id)
    {
        var theatre = await _theatreService.GetTheatreByIdAsync(id);

        await _theatreService.DeleteTheatreAsync(id);
        return new NoContentResult();
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPut("admin/theatres/{id}")]
    public async Task<IActionResult> UpdateTheatre(Guid id, [FromBody] TheatreUpdateRequest request)
    {
        await _theatreService.UpdateTheatreAsync(id, request);

        return new NoContentResult();
    }
}