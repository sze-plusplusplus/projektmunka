using MeetHut.DataAccess.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MeetHut.DataAccess
{
    /// <inheritdoc />
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        /// <inheritdoc />
        public DatabaseContext CreateDbContext(string[] args)
        {
            var connString = DatabaseConfiguration.DesignTimeConnection;
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseMySql(connString, ServerVersion.AutoDetect(connString));

            return new DatabaseContext(optionsBuilder.Options);
        }
    }
}