using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DISLAMS_Assignment.Infrastructure.Persistence
{
    public class AppDBContextFactory
        : IDesignTimeDbContextFactory<AppDBContext>
    {
        public AppDBContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<AppDBContext>();
            optionsBuilder.UseSqlServer(
                configuration.GetConnectionString("AttendanceDb"));

            return new AppDBContext(optionsBuilder.Options);
        }
    }
}
