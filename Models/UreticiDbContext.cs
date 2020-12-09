using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webApiDeneme.Models
{
    public class UreticilerDbContext: DbContext
    {
        public DbSet<Ureticiler> Ureticiler { get; set; }


        public UreticilerDbContext(DbContextOptions<UreticilerDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ureticiler>().ToTable("Ureticiler");
        }
    }
}
