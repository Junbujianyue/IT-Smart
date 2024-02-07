using Microsoft.EntityFrameworkCore;
using NBD3.Models;

namespace NBD3.Data
{
    public class NBDContext : DbContext
    {
        public NBDContext(DbContextOptions<NBDContext> options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Location> Locations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Client project Cascade delete
            modelBuilder.Entity<Client>()
                .HasMany<Project>(c => c.Projects)
                .WithOne(p => p.Client)
                .HasForeignKey(p => p.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            //Client Unique name
            modelBuilder.Entity<Client>()
                .HasIndex(c => c.ClientFirstName)
                .IsUnique();

            //Project Unique name
            modelBuilder.Entity<Project>()
                .HasIndex(p => p.ProjectName)
                .IsUnique();

            //Location Unique name
            modelBuilder.Entity<Location>()
                .HasIndex(l => l.LocationName)
                .IsUnique();
        }
    }
}
