using Microsoft.Azure.Cosmos;
using System;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CosmosDBClientSample
{
    class Program
    {
        private const string endpoint = "https://[your-endpoint].documents.azure.com:443/";
        private const string key = "[yourkey]";

        private const string databaseId = "WWTravelClub";
        private const string containerId = "Destination";

        public static async Task Main()
        {
            try
            {
                await CreateCosmosDB();
            }
            catch (CosmosException de)
            {
                Console.WriteLine("{0} error occurred: {1}", de.StatusCode, de);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e);
            }
            finally
            {
                Console.WriteLine("End of demo, press any key to exit.");
                Console.ReadKey();
            }
        }

        public static async Task CreateCosmosDB()
        {
            using var cosmosClient = new CosmosClient(endpoint, key);
            Database database = await cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
            ContainerProperties cp = new ContainerProperties(containerId, "/DestinationName");
            Container container = await database.CreateContainerIfNotExistsAsync(cp);
            await AddItemsToContainerAsync(container);
        }


        private static async Task AddItemsToContainerAsync(Container container)
        {
            var destinationToAdd = new Destination
            {
                id = "1",
                Country = "Brazil",
                DestinationName = "São Paulo",
                Description = "The biggest city in Brazil",
                Packages = new[]
                {
                    new Package
                    {
                        id = "1",
                        Description = "Visit Paulista Avenue",
                        DurationInDays = 3,
                        Price = 5000,
                    }
                }
            };


            try
            {
                var item = await container.ReadItemAsync<Destination>(destinationToAdd.id, new PartitionKey(destinationToAdd.DestinationName));
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                try
                {
                    await container.CreateItemAsync<Destination>(destinationToAdd, new PartitionKey(destinationToAdd.DestinationName));
                }
                catch (CosmosException err)
                {
                    Console.WriteLine(err.ToString());
                }
            }
        }
    }

    public class Destination
    {
        public string id { get; set; }

        public string DestinationName { get; set; }

        public string Country { get; set; }
        public string Description { get; set; }
        public Package[] Packages { get; set; }
    }

    public class Package
    {
        public string id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        public decimal Price { get; set; }
        public int DurationInDays { get; set; }
        public DateTime? StartValidityDate { get; set; }
        public DateTime? EndValidityDate { get; set; }
        public Destination MyDestination { get; set; }
        public string DestinationId { get; set; }
    }
}
