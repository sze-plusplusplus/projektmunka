using System;
using System.Linq;
using MeetHut.DataAccess.Entities;
using MeetHut.DataAccess.Entities.Meet;
using MeetHut.DataAccess.Enums;
using MeetHut.DataAccess.Enums.Meet;
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
        
        /// <summary>
        /// Rooms
        /// </summary>
        public DbSet<Room> Rooms { get; set; }

        /// <summary>
        /// Room users (mapper connection)
        /// </summary>
        public DbSet<RoomUser> RoomUsers { get; set; }

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

            modelBuilder.Entity<RoomUser>().HasKey(ru => new { ru.RoomId, ru.UserId });

            modelBuilder.Entity<User>().Property(u => u.Role).HasDefaultValue(UserRole.Student);
            modelBuilder.Entity<RoomUser>().Property(u => u.Role).HasDefaultValue(MeetRole.Guest);

            modelBuilder.Entity<RoomUser>().Property(ru => ru.Added).HasDefaultValueSql("NOW()");

            base.OnModelCreating(modelBuilder);
        }

        /// <inheritdoc />
        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is Entity && e.State is EntityState.Added or EntityState.Modified);

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