
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.IO;

namespace MusicCatalog.Infrastructure.EntityFramework
{
    public class MusicCatalogDbContextFactory : IDesignTimeDbContextFactory<MusicCatalogDbContext>
    {
        public MusicCatalogDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json", optional: false)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<MusicCatalogDbContext>();

            var connectionString = configuration.GetConnectionString("MusicCatalogDbContext");

            optionsBuilder.UseNpgsql(connectionString);

            return new MusicCatalogDbContext(optionsBuilder.Options);
        }
    }
}