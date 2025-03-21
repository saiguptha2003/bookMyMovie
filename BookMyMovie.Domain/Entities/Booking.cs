using System;
using System.ComponentModel.DataAnnotations;
namespace BookMyMovie.Domain.Entities;
public class Booking
{
    public Guid Id { get; set; } 
    
    public Guid UserId { get; set; } 
    public User User { get; set; } 
    public Guid ShowTimeId { get; set; } 
    public ScreenShowTime ShowTime { get; set; } 
    public int Count { get; set; }
    public DateTime BookingTime { get; set; }
    public DateTime BookingDate { get; set; }
    public string Seats { get; set; } 
    public Guid SnacksOrder { get; set; }
    public double  TotalAmount { get; set; }
}