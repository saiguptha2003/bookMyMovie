using BookMyMovie.Application.Services.ScreenShowTime;
using BookMyMovie.Application.Services.ScreenShowTime.ScreenShowTimeDTOS;
using BookMyMovie.Contracts.ScreenShowTime;
using BookMyMovie.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookMyMovie.API.Controllers;

[Route("api")]
[ApiController]
public class ScreenShowTimeController : ControllerBase
{
    private readonly IScreenShowTimeService _showTimeService;

    public ScreenShowTimeController(IScreenShowTimeService showTimeService)
    {
        _showTimeService = showTimeService;
    }
    [Authorize(Roles = "Admin")]
    [HttpPost("admin/shows")]
    public async Task<IActionResult> CreateShowTime([FromBody] ScreenShowTimeCreateRequst request)
    {
        if (request == null)
        {
            return BadRequest("Invalid request.");
        }

        await _showTimeService.CreateShowTime(request);
        return Ok("Showtime created successfully.");
    }
    [HttpGet("shows")]
    public async Task<List<ShowTimeResponse>> GetShowTimes([FromQuery] ShowQuery query)
    {
        return await _showTimeService.GetShowTimes(query);
    }
    
    
    [HttpGet("shows/{id}")]
    public async Task<ShowTimeDTO> GetShowById(Guid id)
    {
        return await _showTimeService.GetShowId(id);
    }
    [HttpPut("movies/{showId}/update-booked-seats")]
    public async Task<IActionResult> UpdateBookedSeats(Guid showId, [FromBody] UpdateBookedSeats bookedSeats)
    {
        try
        {
            var updatedSeats = await _showTimeService.UpdateBookedSeats(showId, bookedSeats.BookedSeats);
            return Ok(new { Message = "Booked seats updated.", BookedSeats = updatedSeats });
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("shows/{showId}/get-booked-seats")]
    public async Task<IActionResult> GetBookedSeats(Guid showId)
    {
        try
        {
            var bookedSeats = await _showTimeService.GetBookedSeats(showId);
            return Ok(new { ShowId = showId, BookedSeats = bookedSeats });
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }
    [Authorize(Roles = "Admin")]
    [HttpPut("admin/shows/{showId}/update-show-dates")]
    public async Task<IActionResult> UpdateShowDate(Guid showId, [FromBody] DateOnly showDate)
    {
        try
        {
            var updatedDate = await _showTimeService.UpdateShowDate(showId, showDate);
            return Ok(new { Message = "Show date updated.", ShowDate = updatedDate });
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }
    [Authorize(Roles = "Admin")]
    [HttpPut("admin/shows/{showId}/update-show-times")]
    public async Task<IActionResult> UpdateShowTime(Guid showId, [FromBody] TimeOnly showTime)
    {
        try
        {
            var updatedTime = await _showTimeService.UpdateShowTime(showId, showTime);
            return Ok(new { Message = "Show time updated.", ShowTime = updatedTime });
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }
    
}
