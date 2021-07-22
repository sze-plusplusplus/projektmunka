using System;
using System.Linq;
using MeetHut.DataAccess.Entities;
using MeetHut.DataAccess.Entities.Meet;
using Microsoft.EntityFrameworkCore;

namespace MeetHut.DataAccess
{
    /// <inheritdoc />
    public class DatabaseContext : DbContext
    {
        /// <summary>
        /// Application users
        /// </summary>
        public DbSet<User> Users { get; set; }
        
        public DbSet<Room> Rooms { get; set; }

        /// <summary>
        /// Init Database context
        /// </summary>
        /// <param name="options">Context options</param>
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            
        }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(user => user.UserName)
                .IsUnique();

            modelBuilder.Entity<Room>()
                .HasIndex(room => room.PublicId)
                .IsUnique();
            
            base.OnModelCreating(modelBuilder);
        }

        /// <inheritdoc />
        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is Entity && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((Entity)entityEntry.Entity).LastUpdate = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((Entity)entityEntry.Entity).Creation = DateTime.Now;
                }
            }
            
            return base.SaveChanges();
        }
    }
}