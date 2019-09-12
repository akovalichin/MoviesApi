namespace MoviesApi.Services
{
    public interface IRatingService
    {
        void RateMovie(int movieId, int userId, int rating);
    }
}