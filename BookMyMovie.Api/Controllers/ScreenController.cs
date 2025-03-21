using BookMyMovie.Application.Services.Screens;
using BookMyMovie.Contracts.Screen;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookMyMovie.Api.Controllers;
[Route("api")]
[ApiController]
public class ScreenController: ControllerBase
{
    private readonly IScreenService _screenService;

    public ScreenController(IScreenService screenService)
    {
        _screenService = screenService;
        
    }
    [Authorize(Roles = "Admin")]
    [HttpPost("admin/screens")]
    public async Task<IActionResult> CreateScreen([FromBody] ScreenCreateRequest? request)
    {
        if (request == null)
        {
            return BadRequest("Invalid Screen data");
        }

        var response = await _screenService.ScreenCreateAsync(
            request.TheatreId,
            request.TotalSeats,
            request.SeatLayout
        );
        return Ok(response);
    }

    [HttpGet("screens")]
    public async Task<IActionResult> GetAllScreens()
    {
        var response = await _screenService.GetAllScreensAsync();
        return Ok(response);
    }

    [HttpGet("screens/{id}")]
    public async Task<IActionResult> GetScreen(Guid id)
    {
        var response = await _screenService.GetScreenByIdAsync(id);
        return Ok(response);
    }

    [HttpGet("screens/theatre/{id}")]
    public async Task<IActionResult>GetScreenByTheatreId(Guid id)
    {
        var response = await _screenService.GetScreensByTheatreIdAsync(id);
        return Ok(response);
    }

    [HttpGet("screens/filter/{theatreName}")]
    public async Task<IActionResult> GetScreenByTheatreName(string theatreName)
    {
        var response = await _screenService.GetScreenByTheatreNameAsync(theatreName);
        return Ok(response);
        
    }

    [HttpGet("screens/{id}/number_seats")]
    public async Task<IActionResult> GetTotalNumberOfSeatsAsync(Guid id)
    {
        var response = await _screenService.GetTotalNumberOfSeatsAsync(id);
        return Ok(response);
    }

    [HttpGet("screens/{id}/seat_layout")]
    public async Task<IActionResult> GetSeatLayoutById(Guid id)
    {
        var response = await _screenService.GetSeatLayoutByIdAsync(id);
        return Ok(response);
    }

    // [HttpPatch("{id}")]
    // public async Task<IActionResult> PatchScreens(Guid id)
    // {
    //     await _screenService.DeleteScreen(id);
    //     return new NoContentResult();fdf
    // }
    
    [Authorize(Roles = "Admin")]
    [HttpPut("admin/screens/{id}/seat_layout")]
    public async Task<IActionResult> UpdateSeatLayout(Guid id, SeatLayoutRequest? request)
    {
        var response=await _screenService.UpdateScreenLayout(id, request.SeatLayout);
        return Ok(response);
    }
    [Authorize(Roles = "Admin")]
    [HttpPut("admin/screens/{id}/total_seats")]
    public async Task<IActionResult> UpdateTotalSeats(Guid id, TotalSeatsRequest? request)
    {
        var response = _screenService.UpdateTotalSeats(id, request.TotalSeats);
        return Ok(response);
    }
    
    
}