using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace webApiDeneme.Models
{
    public class SatislarDbContext : DbContext
    {
        public DbSet<Satislar> Satislar { get; set; }


        public SatislarDbContext(DbContextOptions<SatislarDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Satislar>().ToTable("Satislar");
        }
    }
}
