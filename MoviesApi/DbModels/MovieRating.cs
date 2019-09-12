using System;
using System.ComponentModel.DataAnnotations;

namespace MoviesApi.DbModels
{
    public class MovieRating
    {
        [Key]
        public Guid MovieRatingId { get; set; }

        public int MovieId { get; set; }
        public int UserId { get; set; }
        public int Rating { get; set; }
    }
}