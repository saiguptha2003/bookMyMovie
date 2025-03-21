namespace BookMyMovie.Domain.Entities;

using System;
using System.ComponentModel.DataAnnotations;

public class ScreenShowTime
{
    public Guid Id { get; set; }
    
    public Guid ScreenId { get; set; }
    public Screen Screen { get; set; } = null!;  // Navigation property to Screen
    
    public Guid MovieId { get; set; }
    public Movie Movie { get; set; } = null!;
    
    public DateOnly ShowDate { get; set; }
    public TimeOnly ShowTime { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public string BookedSeats { get; set; } = string.Empty;
}
 