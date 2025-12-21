using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WWTravelClubDB
{
    public class LibraryDesignTimeDbContextFactory
        : IDesignTimeDbContextFactory<MainDBContext>
    {
        private const string endpoint = "https://[your-endpoint].documents.azure.com:443/";
        private const string key = "[yourkey]";
        private const string databaseName = "packagesdb";
        public MainDBContext CreateDbContext(params string[] args)
        {
            var builder = new DbContextOptionsBuilder<MainDBContext>();

            builder.UseCosmos(endpoint, key, databaseName);
            return new MainDBContext(builder.Options);
        }
    }
}
