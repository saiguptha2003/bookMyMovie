using BookMyMovie.Application.Common.Services;

namespace BookMyMovie.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
    
}