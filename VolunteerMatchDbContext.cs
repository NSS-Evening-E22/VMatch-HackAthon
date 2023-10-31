﻿using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection.Metadata;
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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var gameTeam = modelBuilder.Entity("GameTeam");
        }
    }
}
