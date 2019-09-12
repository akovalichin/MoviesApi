using MoviesApi.Repos;

namespace MoviesApi.Services
{
    public class RatingService : IRatingService
    {
        private IMoviesRepo _moviesRepo;

        public RatingService(IMoviesRepo moviesRepo)
        {
            _moviesRepo = moviesRepo;
        }

        public void RateMovie(int movieId, int userId, int rating)
        {
            _moviesRepo.RateMovie(movieId, userId, rating);
        }
    }
}