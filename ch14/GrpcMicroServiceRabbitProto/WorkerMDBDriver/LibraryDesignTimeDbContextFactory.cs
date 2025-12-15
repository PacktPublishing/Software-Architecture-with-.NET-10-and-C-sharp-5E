using DBDriver;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Net.Mime.MediaTypeNames;

namespace WorkerMDBDriver
{
    internal class LibraryDesignTimeDbContextFactory
    : IDesignTimeDbContextFactory<MainDbContext>
    {
        private const string connectionString =
        @"Server=(localdb)\MSSQLLocalDB;Database=workermicroservice;Trusted_Connection=True;MultipleActiveResultSets=true";
        public MainDbContext CreateDbContext(params string[] args)
        {
            var builder = new DbContextOptionsBuilder<MainDbContext>();
            builder.UseSqlServer(connectionString);
            return new MainDbContext(builder.Options);
        }
    }
}
