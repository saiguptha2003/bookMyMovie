using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BookMyMovie.Application.Services.Booking;
using BookMyMovie.Application.Services.Booking.BookingDTOs;
using BookMyMovie.Contracts.Booking;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookMyMovie.Api.Controllers;

[ApiController]
[Route("api")]
public class BookingController : ControllerBase
{
    private readonly IBookingService _bookingService;
    
    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }
    
    // Helper method to get user ID consistently
    private Guid? GetUserId()
    {
        // Try multiple claim types that might contain the user ID
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? 
                           User.FindFirstValue(JwtRegisteredClaimNames.Sub) ??
                           User.FindFirstValue("sub");

        if (string.IsNullOrEmpty(userIdString))
        {
            // Log all claims for debugging
            System.Console.WriteLine("Available claims:");
            foreach (var claim in User.Claims)
            {
                System.Console.WriteLine($"Claim Type: {claim.Type}, Value: {claim.Value}");
            }
            return null;
        }

        if (Guid.TryParse(userIdString, out Guid userId))
        {
            return userId;
        }
        
        return null;
    }
    
    [Authorize(Roles = "Ticketee")]
    [HttpPost("user/bookings")]
    public async Task<IActionResult> CreateBooking([FromBody] CreateBookingRequest request)
    {
        if (request == null)
        {
            return BadRequest("Invalid booking data.");
        }
        
        var userId = GetUserId();
        if (!userId.HasValue)
        {
            return Unauthorized("Unable to identify user from token.");
        }
        
        System.Console.WriteLine($"Creating booking for user: {userId}");
        
        try
        {
            await _bookingService.CreateBooking(
                userId.Value, 
                request.ShowTimeId, 
                request.Count, 
                request.Seats, 
                request.Amount);
                
            return Ok("Booking created successfully.");
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"Error creating booking: {ex.Message}");
            return StatusCode(500, "Error creating booking");
        }
    }
    
    [Authorize(Roles = "Admin")]
    [HttpDelete("admin/bookings/{id}")]
    public async Task<IActionResult> DeleteBooking(Guid id)
    {
        if (id == Guid.Empty)
        {
            return BadRequest("Invalid booking ID.");
        }
        
        try
        {
            await _bookingService.DeleteBooking(id);
            return Ok("Booking deleted successfully.");
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"Error deleting booking: {ex.Message}");
            return StatusCode(500, "Error deleting booking");
        }
    }
    
    // [Authorize(Roles = "Admin")]
    // [HttpGet("admin/bookings")]
    // public async Task<IActionResult> GetAllBookings()
    // {
    //     try
    //     {
    //         var bookings = await _bookingService.GetAllBookings();
    //         return Ok(bookings);
    //     }
    //     catch (Exception ex)
    //     {
    //         System.Console.WriteLine($"Error retrieving all bookings: {ex.Message}");
    //         return StatusCode(500, "Error retrieving bookings");
    //     }
    // }
    //
    [Authorize(Roles = "Admin")]
    [HttpGet("admin/bookings/{id}")]
    public async Task<IActionResult> GetBookingById(Guid id)
    {
        if (id == Guid.Empty)
        {
            return BadRequest("Invalid booking ID.");
        }
        
        try
        {
            var booking = await _bookingService.GetBookingById(id);
            if (booking == null)
            {
                return NotFound("Booking not found");
            }
            return Ok(booking);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"Error retrieving booking by ID: {ex.Message}");
            return StatusCode(500, "Error retrieving booking");
        }
    }
    
    [Authorize(Roles = "Ticketee")]
    [HttpGet("user/bookings")]
    public async Task<IActionResult> GetAllBookingsByUserId()
    {
        var userId = GetUserId();
        if (!userId.HasValue)
        {
            return Unauthorized("Unable to identify user from token.");
        }
        
        System.Console.WriteLine($"Retrieving bookings for user: {userId}");
        
        try
        {
            var bookings = await _bookingService.GetAllBookingsByUserId(userId.Value);
            return Ok(bookings);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"Error retrieving user bookings: {ex.Message}");
            return StatusCode(500, "Error retrieving bookings");
        }
    }
    
    [Authorize(Roles = "Admin")]
    [HttpGet("admin/shows/{showId}")]
    public async Task<IActionResult> GetAllBookingsByShowId(Guid showId)
    {
        if (showId == Guid.Empty)
        {
            return BadRequest("Invalid show ID.");
        }
        
        try
        {
            var bookings = await _bookingService.GetAllBookingsByShowId(showId);
            return Ok(bookings);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"Error retrieving bookings by show ID: {ex.Message}");
            return StatusCode(500, "Error retrieving bookings");
        }
    }
}