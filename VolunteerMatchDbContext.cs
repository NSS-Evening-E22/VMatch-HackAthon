using Microsoft.EntityFrameworkCore;
using Volunteer_Match_Backend.Models;

namespace Volunteer_Match_Backend
{
    public class VolunteerMatchDbContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<TeamGame> TeamGames { get; set; } 
        public DbSet<Team> Teams { get; set; }
        public DbSet<Volunteer> Volunteers { get; set; }
        public VolunteerMatchDbContext(DbContextOptions<VolunteerMatchDbContext> context) : base(context)
        {

        }

    }
}
