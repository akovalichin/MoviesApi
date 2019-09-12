using System.Collections.Generic;
using MoviesApi.DbModels;

namespace MoviesApi.Services
{
    public interface IMoviesService
    {
        List<Movie> GetMoviesByFilter(string title, int? year, string genre);

        List<Movie> GetTopRated();

        List<Movie> GetTopRatedByUser(int userId);
    }
}