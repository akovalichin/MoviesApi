using System;
using System.ComponentModel.DataAnnotations;

namespace MoviesApi.Models
{
    public class MovieRatingRequest
    {
        [Range(1, Int32.MaxValue)]
        public int userId { get; set; }

        [Range(1, 5)]
        public int rating { get; set; }

        [Range(1, Int32.MaxValue)]
        public int movieId { get; set; }
    }
}