using Microsoft.EntityFrameworkCore;

namespace BeerApi
{
    public class BeersDbContext : DbContext 
    {
        public DbSet<Beer> Beers {get; set;}

        public BeersDbContext(DbContextOptions<BeersDbContext> options) : base(options)
        {
            
        }
    }
}