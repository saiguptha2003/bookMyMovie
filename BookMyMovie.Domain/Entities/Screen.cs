using System.ComponentModel.DataAnnotations;

using System;
using System.Collections.Generic;
namespace BookMyMovie.Domain.Entities
{
    public class Screen
    {
        public Guid Id { get; set; } // Primary key

        public Guid TheatreId { get; set; } // Foreign key
        public Theatre Theatre { get; set; } // Navigation property
        public int TotalSeats { get; set; }
        public string SeatLayout { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        // This is the navigation property to represent the many-to-many relationship.
        public ICollection<ScreenShowTime> ScreenShowTimes { get; set; }
    }
}