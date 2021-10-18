using System;
using System.Linq;
using MeetHut.DataAccess.Entities;
using MeetHut.DataAccess.Entities.Meet;
using MeetHut.DataAccess.Enums;
using MeetHut.DataAccess.Enums.Meet;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MeetHut.DataAccess
{
    /// <inheritdoc />
    public class DatabaseContext : IdentityDbContext<User, Role, int>
    {        
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
            base.OnModelCreating(modelBuilder);

            // User
            modelBuilder.Entity<User>()
                .HasIndex(user => user.UserName)
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(user => user.Email)
                .IsUnique();

            // Room
            modelBuilder.Entity<Room>()
                .HasIndex(room => room.PublicId)
                .IsUnique();
            modelBuilder.Entity<Room>()
                .HasOne(x => x.Owner)
                .WithMany(x => x.OwnedRooms)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientCascade);
            
            // Room user
            modelBuilder.Entity<RoomUser>().HasKey(ru => new { ru.RoomId, ru.UserId });
            modelBuilder.Entity<RoomUser>().Property(u => u.Role).HasDefaultValue(MeetRole.Guest);
            modelBuilder.Entity<RoomUser>().Property(ru => ru.Added).HasDefaultValueSql("NOW()");
            modelBuilder.Entity<RoomUser>()
                .HasOne(x => x.Room)
                .WithMany(x => x.RoomUsers)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientCascade);
            modelBuilder.Entity<RoomUser>()
                .HasOne(x => x.User)
                .WithMany(x => x.RoomUsers)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientCascade);
            modelBuilder.Entity<RoomUser>()
                .HasOne(x => x.Adder)
                .WithMany(x => x.AddedRoomUsers)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }

        /// <inheritdoc />
        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is Entity && e.State is EntityState.Added or EntityState.Modified);

            var now = DateTime.Now;

            foreach (var entityEntry in entries)
            {
                ((Entity)entityEntry.Entity).LastUpdate = now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((Entity)entityEntry.Entity).Creation = now;
                }
            }

            return base.SaveChanges();
        }
    }
}