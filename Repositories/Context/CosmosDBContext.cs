using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Context
{
    public class CosmosDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Mortgage> Mortgages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
           => optionsBuilder.UseCosmos(
               "https://localhost:8081",
               "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==",
               databaseName: "BuyMyHouse");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultContainer("Containers");

            modelBuilder.Entity<User>()
                .ToContainer("users");

            modelBuilder.Entity<User>()
               .HasNoDiscriminator();

            modelBuilder.Entity<User>()
                .UseETagConcurrency();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Mortgage>()
                .HasOne(m => m.User)
                .WithOne(u => u.Mortgage);

            modelBuilder.Entity<Mortgage>()
                .ToContainer("mortgages");

            modelBuilder.Entity<Mortgage>()
               .HasNoDiscriminator();

            modelBuilder.Entity<Mortgage>()
                .UseETagConcurrency();
        }
    }
}
