using System;
using Microsoft.EntityFrameworkCore;

namespace MoviesApi.DbModels
{
    public class InMemoryContext : DbContext
    {
        public InMemoryContext(DbContextOptions<InMemoryContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<MovieRating> Ratings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().HasData(new Movie { Id = 1, Title = "Rambo", Genre = "Action", YearOfRelease = 1995, RunningTime = 120 });
            modelBuilder.Entity<Movie>().HasData(new Movie { Id = 2, Title = "Rambo 2", Genre = "Drama", YearOfRelease = 1998, RunningTime = 128 });
            modelBuilder.Entity<Movie>().HasData(new Movie { Id = 3, Title = "Rambo 3", Genre = "Action", YearOfRelease = 2000, RunningTime = 100 });
            modelBuilder.Entity<Movie>().HasData(new Movie { Id = 4, Title = "Rambo 4", Genre = "Drama", YearOfRelease = 2003, RunningTime = 135 });
            modelBuilder.Entity<Movie>().HasData(new Movie { Id = 5, Title = "Rambo 5", Genre = "Comedy", YearOfRelease = 2010, RunningTime = 125 });
            modelBuilder.Entity<Movie>().HasData(new Movie { Id = 6, Title = "Ghostbusters", Genre = "Horror", YearOfRelease = 1987, RunningTime = 145 });

            modelBuilder.Entity<User>().HasData(new User { FirstName = "John", LastName = "Smith", Id = 1, Username = "jsmith" });
            modelBuilder.Entity<User>().HasData(new User { FirstName = "Jane", LastName = "Doe", Id = 2, Username = "jdoe" });
            modelBuilder.Entity<User>().HasData(new User { FirstName = "Jim", LastName = "Carrey", Id = 3, Username = "jcarrey" });
            modelBuilder.Entity<User>().HasData(new User { FirstName = "Homer", LastName = "Simpson", Id = 4, Username = "jdoe" });

            modelBuilder.Entity<MovieRating>().HasData(new MovieRating { MovieRatingId = Guid.NewGuid(), MovieId = 1, UserId = 1, Rating = 5 });
            modelBuilder.Entity<MovieRating>().HasData(new MovieRating { MovieRatingId = Guid.NewGuid(), MovieId = 2, UserId = 1, Rating = 2 });
            modelBuilder.Entity<MovieRating>().HasData(new MovieRating { MovieRatingId = Guid.NewGuid(), MovieId = 3, UserId = 1, Rating = 3 });
            modelBuilder.Entity<MovieRating>().HasData(new MovieRating { MovieRatingId = Guid.NewGuid(), MovieId = 4, UserId = 1, Rating = 4 });
            modelBuilder.Entity<MovieRating>().HasData(new MovieRating { MovieRatingId = Guid.NewGuid(), MovieId = 5, UserId = 1, Rating = 1 });
            modelBuilder.Entity<MovieRating>().HasData(new MovieRating { MovieRatingId = Guid.NewGuid(), MovieId = 6, UserId = 1, Rating = 3 });

            modelBuilder.Entity<MovieRating>().HasData(new MovieRating { MovieRatingId = Guid.NewGuid(), MovieId = 1, UserId = 2, Rating = 5 });
            modelBuilder.Entity<MovieRating>().HasData(new MovieRating { MovieRatingId = Guid.NewGuid(), MovieId = 2, UserId = 2, Rating = 4 });
            modelBuilder.Entity<MovieRating>().HasData(new MovieRating { MovieRatingId = Guid.NewGuid(), MovieId = 3, UserId = 2, Rating = 3 });
            modelBuilder.Entity<MovieRating>().HasData(new MovieRating { MovieRatingId = Guid.NewGuid(), MovieId = 4, UserId = 2, Rating = 2 });
            modelBuilder.Entity<MovieRating>().HasData(new MovieRating { MovieRatingId = Guid.NewGuid(), MovieId = 5, UserId = 2, Rating = 1 });
            modelBuilder.Entity<MovieRating>().HasData(new MovieRating { MovieRatingId = Guid.NewGuid(), MovieId = 6, UserId = 2, Rating = 3 });

            modelBuilder.Entity<MovieRating>().HasData(new MovieRating { MovieRatingId = Guid.NewGuid(), MovieId = 3, UserId = 3, Rating = 2 });

            modelBuilder.Entity<MovieRating>().HasData(new MovieRating { MovieRatingId = Guid.NewGuid(), MovieId = 3, UserId = 4, Rating = 1 });
        }
    }
}