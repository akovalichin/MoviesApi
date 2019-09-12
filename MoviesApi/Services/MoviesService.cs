using System.Collections.Generic;
using MoviesApi.DbModels;
using MoviesApi.Repos;

namespace MoviesApi.Services
{
    public class MoviesService : IMoviesService
    {
        private IMoviesRepo _moviesRepo;

        public MoviesService(IMoviesRepo moviesRepo)
        {
            _moviesRepo = moviesRepo;
        }

        public List<Movie> GetMoviesByFilter(string title, int? year, string genre)
        {
            return _moviesRepo.GetMoviesByFilter(title, year, genre);
        }

        public List<Movie> GetTopRated()
        {
            return _moviesRepo.GetTopRated();
        }

        public List<Movie> GetTopRatedByUser(int userId)
        {
            return _moviesRepo.GetTopRatedByUser(userId);
        }
    }
}