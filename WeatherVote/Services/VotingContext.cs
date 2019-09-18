using Microsoft.EntityFrameworkCore;
using WeatherVote.Models;

namespace WeatherVote.Services
{
    public class VotingContext:DbContext
    {
        public VotingContext(DbContextOptions<VotingContext> options)
           : base(options)
        {
        }

        public DbSet<Voting> Votes { get; set; }
        public DbSet<WeatherSupplier> WeatherSuppliers { get; set; }
    


    }
}
