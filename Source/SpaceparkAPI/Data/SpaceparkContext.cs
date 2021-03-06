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
        public SpaceParkContext(DbContextOptions<SpaceParkContext> options) : base(options)
        {
        }

        public virtual DbSet<Parking> Parkings { get; set; }
        public virtual DbSet<SpacePort> SpacePorts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Parking>()
                .Property(x => x.StartTime)
                .HasDefaultValueSql("getdate()");
        }
    }
}
