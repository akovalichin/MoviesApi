using System.ComponentModel.DataAnnotations;

namespace MoviesApi.DbModels
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }
        public int YearOfRelease { get; set; }
        public int RunningTime { get; set; }
        public string Genre { get; set; }
        public double AverageRating { get; set; }
    }
}