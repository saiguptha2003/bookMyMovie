using System.Text;
using BookMyMovie.Application;
using BookMyMovie.Application.Services.Authentication;
using BookMyMovie.Infrastructure;
using BookMyMovie.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container
    builder.Services.AddControllers();
    builder.Services.AddApplication().AddInfrastructure(builder.Configuration);
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "BookMyMovie",
                ValidAudience = "BookMyMovie",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ZYuX08RlTXGzC29OfontAHzHG2S5zDAOUYG/WZs6fhvim+u7Y8DXd3BZhcS4B0Sc"))
            };
        });
    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    });
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
        // Add Security Definition for JWT Bearer token
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
Enter 'Bearer' [space] and then your token in the text input below
\r\n\r\nExample: 'Bearer 12345abcdef'",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                },
                new List<string>()
            }
        });
    });
    
    // CORS configuration that will allow requests from specific origins with credentials
    builder.Services.AddCors();
}

var app = builder.Build();
{
    // Configure the HTTP request pipeline
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            c.RoutePrefix = string.Empty; // Makes Swagger UI available at root URL
        });
    }
    
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<BookMyMovieContext>();
        // dbContext.Database.EnsureCreated();   juijuhin
        // dbContext.Database.Migrate();       
    }
    
    app.Urls.Add("http://0.0.0.0:5000");
    app.UseRouting();
    
app.Use(async (context, next) =>
    {
        var origin = context.Request.Headers.Origin.ToString();
        
        // Allow any origin with credentials
        if (!string.IsNullOrEmpty(origin))
        {
            context.Response.Headers.Append("Access-Control-Allow-Origin", origin);
            context.Response.Headers.Append("Access-Control-Allow-Credentials", "true");
            context.Response.Headers.Append("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
            context.Response.Headers.Append("Access-Control-Allow-Headers", "Content-Type, Accept, Authorization, X-Requested-With");
            
            // Handle preflight requests
            if (context.Request.Method == "OPTIONS")
            {
                context.Response.StatusCode = 200;
                await context.Response.CompleteAsync();
                return;
            }
        }
        
        await next();
    });    // Custom middleware to handle CORS for all origins with credentials
    
    
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}