using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using MonkeyShelter.Data.Model;

namespace MonkeyShelter.Data
{
    public class ShelterContext : DbContext
    {
        public ShelterContext(DbContextOptions<ShelterContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Monkey>()
                .HasKey(x => x.Id)
                ;
            modelBuilder.Entity<Monkey>().HasData(MonkeyCollection.Monkeys);
        }

        public DbSet<Monkey> Monkey { get; set; }
    }
}
