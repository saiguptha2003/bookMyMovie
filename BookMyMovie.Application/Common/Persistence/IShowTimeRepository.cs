using BookMyMovie.Contracts.ScreenShowTime;
using BookMyMovie.Domain.Entities;

namespace BookMyMovie.Application.Common.Persistence;

public interface IShowTimeRepository
{
    
    public Task AddAsync(
ScreenShowTime showTime
        );
    public Task<List<ScreenShowTime>> ScreenShowTimes(ShowQuery query);
    
    public Task<List<ScreenShowTime>> ScreenShowTimesByMovieId(Guid movieId);
    
    public Task<ScreenShowTime?> ScreenShowTimesByScreenId(Guid screenId);
    public Task<List<ScreenShowTime>> ScreenShowTimeByTheatreId(Guid theatreId);
    
    public Task<string> UpdateBookedSeats(Guid screenId, string bookedSeats);
    public Task<Movie> GetMovieByScreenId(Guid id);
    public Task<Theatre> GetTheatreByScreenId(Guid id);
	public Task UpdateAsync(ScreenShowTime showTime);
    public Task<ScreenShowTime> GetShowTimeByID(Guid screenId);

}