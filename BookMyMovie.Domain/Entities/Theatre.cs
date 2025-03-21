using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace BookMyMovie.Domain.Entities;
public class Theatre
{
    public Guid Id { get; set; } // Primary key
    
    [Required]
    [StringLength(200)]
    public string Name { get; set; }

    [Required]
    public string Location { get; set; }
    
    public string Address { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public ICollection<Screen> Screens { get; set; } // Navigation property
    public ICollection<ScreenShowTime> ShowTimes { get; set; } // Navigation property
}