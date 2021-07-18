using MeetHut.DataAccess.Entities;
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
            
            base.OnModelCreating(modelBuilder);
        }
    }
}