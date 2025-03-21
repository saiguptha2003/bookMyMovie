
using BookMyMovie.Application.Common.Persistence;
using Microsoft.Extensions.DependencyInjection;
using BookMyMovie.Application.Services.Authentication;
using BookMyMovie.Application.Services.Booking;
using BookMyMovie.Application.Services.Movies;
using BookMyMovie.Application.Services.Screens;
using BookMyMovie.Application.Services.ScreenShowTime;
using BookMyMovie.Application.Services.Theatres;
using BookMyMovie.Domain.Entities;

namespace BookMyMovie.Application;

public static  class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<ITheatreService, TheatreService>();
        services.AddScoped<IMovieService, MovieService>();
        services.AddScoped<IScreenService, ScreenService>();
        services.AddScoped<IScreenShowTimeService, ScreenShowTimeService>();
        services.AddScoped<IBookingService, BookingService>();
        
        return services;
        
    }
}
