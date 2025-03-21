using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace BookMyMovie.Domain.Entities;
public class User
{
    public Guid Id { get; set; }
    [Required]
    [StringLength(100)]
    public string Username { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; }
    
    [Required]
    [StringLength(50)]
    public string LastName { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Password { get; set; }
    
    public string Role { get; set; }
    
    [Phone]
    public string PhoneNumber { get; set; }
    
    [Required]
    public string Location { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public ICollection<Booking> Bookings { get; set; } // Navigation property
}