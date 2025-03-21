using BookMyMovie.Application.Services.ScreenShowTime.ScreenShowTimeDTOS;
using BookMyMovie.Contracts.ScreenShowTime;
using BookMyMovie.Domain.Entities;

namespace BookMyMovie.Application.Services.ScreenShowTime;

public interface IScreenShowTimeService
{
    public Task CreateShowTime(ScreenShowTimeCreateRequst request);
    public Task<List<ShowTimeResponse>> GetShowTimes(ShowQuery query);
    public Task<string> UpdateBookedSeats(Guid screenId, string bookedSeats);
    public Task<string> GetBookedSeats(Guid showId);
    public Task<DateOnly> UpdateShowDate(Guid showId, DateOnly showDate);
    public Task<TimeOnly> UpdateShowTime(Guid showId, TimeOnly showTime);
    public Task<Movie> GetMovieByShowTime(Guid showId);
    public Task<ShowTimeDTO> GetShowId(Guid showId);
  //  public Task<List<ShowTimeResponse>> GetShowTimesBYMovieIdAndLocation(Guid moviId, string Location);
}