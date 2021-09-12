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
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            var connStr = "server=localhost;port=3306;database=MeetHutDb;user=root;password=";
            optionsBuilder.UseMySql(connStr, ServerVersion.AutoDetect(connStr));

            return new DatabaseContext(optionsBuilder.Options);
        }
    }
}