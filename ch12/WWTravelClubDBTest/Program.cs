using System;
using System.Collections.Generic;
using WWTravelClubDB;
using WWTravelClubDB.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
namespace WWTravelClubDBTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("program start: populate database");
            Console.ReadKey();

            var context = new LibraryDesignTimeDbContextFactory()
                .CreateDbContext();
            await context.Database.EnsureCreatedAsync();
            var firstDestination = new Destination
            {
                id = Guid.NewGuid().ToString(),
                Name = "Florence",
                Country = "Italy",
                Packages = new List<Package>()
            {
                new Package
                {
                    id=Guid.NewGuid().ToString(),
                    Name = "Summer in Florence",
                    StartValidityDate = new DateTime(2019, 6, 1),
                    EndValidityDate = new DateTime(2019, 10, 1),
                    DurationInDays=7,
                    Price=1000
                },
                new Package
                {
                    id=Guid.NewGuid().ToString(),
                    Name = "Winter in Florence",
                    StartValidityDate = new DateTime(2019, 12, 1),
                    EndValidityDate = new DateTime(2020, 2, 1),
                    DurationInDays=7,
                    Price=500
                }
            }
            };
            context.Destinations.Add(firstDestination);
            await context.SaveChangesAsync();
            Console.WriteLine(
                "DB populated: first destination id is " +
                firstDestination.id);
            Console.ReadKey();

             var toModify = await context.Destinations
                .Where(m => m.Name == "Florence")
                .FirstOrDefaultAsync();

            toModify.Description = 
                            "Florence is a famous historical Italian town";
            foreach (var package in toModify.Packages)
                            package.Price = package.Price * 1.1m;
            await context.SaveChangesAsync();

            var verifyChanges= await context.Destinations
                    .Where(m => m.Name == "Florence")
                    .FirstOrDefaultAsync();

            Console.WriteLine(
                "New Florence description: " +
                verifyChanges.Description);
            Console.ReadKey();
            var period = new DateTime(2019, 8, 10);
            var list = await context.Destinations
                .AsAsyncEnumerable()
                .SelectMany(m => m.Packages)
                .Where(m => period >= m.StartValidityDate
                && period <= m.EndValidityDate)
                .Select(m => new PackagesListDTO
                {
                    StartValidityDate=m.StartValidityDate,
                    EndValidityDate=m.EndValidityDate,
                    Name=m.Name,
                    DurationInDays=m.DurationInDays,
                    id=m.id,
                    Price=m.Price,
                    DestinationName=m.MyDestination.Name
                })
                .ToListAsync();
            foreach (var result in list)
                Console.WriteLine(result.ToString());
            Console.ReadKey();
            
        }
        
    }
}
