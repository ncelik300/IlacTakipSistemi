using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace webApiDeneme.Models
{
    public class UrunlerDbContext : DbContext
    {
        public DbSet<Urunler> Urunler { get; set; }


        public UrunlerDbContext(DbContextOptions<UrunlerDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Urunler>().ToTable("Urunler");
        }
    }
}
