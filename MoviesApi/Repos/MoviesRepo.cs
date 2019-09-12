using System;
using System.Collections.Generic;
using System.Linq;
using MoviesApi.DbModels;
using MoviesApi.Exceptions;
using MoviesApi.Services;

namespace MoviesApi.Repos
{
    public class MoviesRepo : IMoviesRepo
    {
        private InMemoryContext _context;
        private IRoundingService _roundingService;

        public MoviesRepo(InMemoryContext context, IRoundingService roundingService)
        {
            _roundingService = roundingService;
            _context = context;
            _context.Database.EnsureCreated();
        }

        public List<Movie> GetMoviesByFilter(string title, int? year, string genre)
        {
            IQueryable<Movie> result = _context.Movies;

            if (!string.IsNullOrEmpty(title))
            {
                result = result.Where(m => m.Title.Contains(title));
            }

            if (!string.IsNullOrEmpty(genre))
            {
                result = result.Where(m => m.Genre == genre);
            }

            if (year != null && year > 0)
            {
                result = result.Where(m => m.YearOfRelease == year);
            }

            return result.ToList();
        }

        public List<Movie> GetTopRated()
        {
            var ratings = (from r in _context.Ratings
                           group r by r.MovieId into g
                           select new
                           {
                               Id = g.First().MovieId,
                               AverageRating = g.Average(_ => _.Rating)
                           });

            var result = (from m in _context.Movies
                          join r in ratings on m.Id equals r.Id
                          orderby r.AverageRating descending
                          select new Movie
                          {
                              Title = m.Title,
                              Genre = m.Genre,
                              YearOfRelease = m.YearOfRelease,
                              AverageRating = _roundingService.RoundToNearestHalf(r.AverageRating),
                              Id = m.Id,
                              RunningTime = m.RunningTime
                          }).Take(5).ToList();

            return result;
        }

        public List<Movie> GetTopRatedByUser(int userId)
        {
            var result = (from m in _context.Movies
                          join r in _context.Ratings on m.Id equals r.MovieId
                          where r.UserId == userId
                          orderby r.Rating descending
                          select m).Take(5).ToList();
            return result;
        }

        public void RateMovie(int movieId, int userId, int rating)
        {
            var currentRating = _context.Ratings.SingleOrDefault(e => e.UserId == userId && e.MovieId == movieId);
            if (currentRating != null)
            {
                currentRating.Rating = rating;
            }
            else
            {
                var user = _context.Users.Where(u => u.Id == userId).SingleOrDefault();
                if (user == null)
                    throw new RepositoryException(RepositoryException.ExeptionType.NotFound, "Movie you trying to rate doesnt exist.");

                var movie = _context.Movies.Where(m => m.Id == movieId).SingleOrDefault();
                if (movie == null)
                    throw new RepositoryException(RepositoryException.ExeptionType.NotFound, "User you trying to rate movie for doesnt exist.");

                _context.Ratings.Add(new MovieRating { MovieRatingId = Guid.NewGuid(), MovieId = movieId, UserId = userId, Rating = rating });
            }
            _context.SaveChanges();
        }
    }
}