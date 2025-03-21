using BookMyMovie.Domain.Entities;

namespace BookMyMovie.Application.Common.Persistence;

public interface IBookingRepository
{
    public Task AddAsync(Booking booking);
    public Task<Booking> GetByIdAsync(Guid BId);
    public Task UpdateAsync(Booking booking);
    public Task DeleteAsync(Booking booking);
    public Task<List<Booking>> BookingListAsync(Guid BId);
    public Task<List<Booking>> BookingListByUserId(Guid BId);
    
    public Task<List<Booking>> BookingListByShowId(Guid SId);
    



}