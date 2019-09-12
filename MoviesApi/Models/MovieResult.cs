namespace MoviesApi.Models
{
    public class MovieResult
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int YearOfRelease { get; set; }
        public int RunningTime { get; set; }
        public float AverageRating { get; set; }
        public string Genre { get; set; }
        public double Rating { get; set; }
    }
}