
using BookMyMovie.Application.Common.Authentication;
using BookMyMovie.Application.Common.Persistence;
using BookMyMovie.Application.Common.Services;
using BookMyMovie.Application.Services.ScreenShowTime;
using BookMyMovie.Infrastructure.Authentication;
using BookMyMovie.Infrastructure.Persistence;
using BookMyMovie.Infrastructure.Repository;
using BookMyMovie.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace BookMyMovie.Infrastructure;

public static  class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        ConfigurationManager builderConfiguration)
    {
        services.Configure<JwtSettings>(builderConfiguration.GetSection(JwtSettings.SectionName));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITheatreRepository, TheatreRepository>();
        services.AddScoped<IMovieRepository, MovieRepository>();
        services.AddScoped<IScreenRepository, ScreenRepository>();
        services.AddScoped<IShowTimeRepository, ShowTimeRepository>();
        services.AddScoped<IBookingRepository, BookingRepository>();
        services.AddDbContext<BookMyMovieContext>(options =>
            options.UseNpgsql("Host=localhost;Port=5432;Database=bookmymovie;Username=postgres;Password=postgres"));
        // var connectionString ="Data Source=bookmymovie.db";
        // services.AddDbContext<BookMyMovieContext>(options =>
        //     options.UseSqlite(connectionString));

        return services;
    }
}
