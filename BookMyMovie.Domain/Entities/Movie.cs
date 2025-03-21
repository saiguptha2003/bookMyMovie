using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace BookMyMovie.Domain.Entities;
public class Movie
{
    public Guid Id { get; set; } // Primary key
    
    [Required]
    [StringLength(200)]
    public string Name { get; set; }
    
    public string PosterUrl { get; set; }
    
    public string Description { get; set; }
    
    public DateTime ReleaseDate { get; set; }
    
    public double Rating { get; set; }
    
    public int Likes { get; set; }
    
    public string Genre { get; set; }
    
    public string Cast { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public ICollection<ScreenShowTime> ShowTimes { get; set; }
}