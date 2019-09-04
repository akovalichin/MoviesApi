using Microsoft.EntityFrameworkCore;

namespace MoviesApi.DbModels
{
    public class InMemoryContext : DbContext
    {
        public InMemoryContext(DbContextOptions<InMemoryContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }

        //public DbSet<Post> Posts { get; set; }
    }
}