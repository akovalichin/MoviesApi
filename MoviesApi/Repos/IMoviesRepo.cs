using System.Collections.Generic;
using MoviesApi.DbModels;

namespace MoviesApi.Repos
{
    public interface IMoviesRepo
    {
        List<Movie> GetMoviesByFilter(string title, int? year, string genre);

        List<Movie> GetTopRated();

        List<Movie> GetTopRatedByUser(int userId);

        void RateMovie(int movieId, int userId, int rating);
    }
}