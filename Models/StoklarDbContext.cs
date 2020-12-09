using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace webApiDeneme.Models
{
    public class StoklarDbContext : DbContext
    {
        public DbSet<Stoklar> Stoklar { get; set; }


        public StoklarDbContext(DbContextOptions<StoklarDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stoklar>().ToTable("Stoklar");
        }
    }
}
