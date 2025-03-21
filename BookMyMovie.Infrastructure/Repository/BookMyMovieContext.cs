using Microsoft.EntityFrameworkCore;
using BookMyMovie.Domain.Entities;

namespace BookMyMovie.Infrastructure.Repository
{
    public class BookMyMovieContext : DbContext
    {
        public BookMyMovieContext(DbContextOptions<BookMyMovieContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Movie?> Movies { get; set; }
        public DbSet<Theatre> Theatres { get; set; }
        public DbSet<Screen> Screens { get; set; }
        public DbSet<ScreenShowTime?> ScreenShowTimes { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<Movie>().HasKey(m => m.Id);
            modelBuilder.Entity<Theatre>().HasKey(t => t.Id);
            modelBuilder.Entity<Screen>().HasKey(s => s.Id);
            modelBuilder.Entity<ScreenShowTime>().HasKey(sst => sst.Id);
            modelBuilder.Entity<Booking>().HasKey(b => b.Id);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId);
            
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.ShowTime)
                .WithMany()
                .HasForeignKey(b => b.ShowTimeId);
            
            modelBuilder.Entity<Screen>()
                .HasOne(s => s.Theatre)
                .WithMany(t => t.Screens)
                .HasForeignKey(s => s.TheatreId);
            
            modelBuilder.Entity<ScreenShowTime>()
                .HasOne(sst => sst.Movie)
                .WithMany(m => m.ShowTimes)
                .HasForeignKey(sst => sst.MovieId);

            modelBuilder.Entity<ScreenShowTime>()
                .HasOne(sst => sst.Screen)
                .WithMany()
                .HasForeignKey(sst => sst.ScreenId)
                .OnDelete(DeleteBehavior.Cascade)
                ;
        }
    }
}
