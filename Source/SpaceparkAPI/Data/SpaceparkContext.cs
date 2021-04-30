using Microsoft.EntityFrameworkCore;
using SpaceparkAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace SpaceParkAPI.Data
{
    public class SpaceParkContext : DbContext
    {
        public DbSet<Parking> Parkings { get; set; }
        public DbSet<SpacePort> SpacePorts { get; set; }
        public SpaceParkContext(DbContextOptions<SpaceParkContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Parking>()
                .Property(x => x.StartTime)
                .HasDefaultValueSql("getdate()");
        }
    }
}
