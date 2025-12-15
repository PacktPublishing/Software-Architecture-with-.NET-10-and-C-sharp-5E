using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using GrpcMicroService;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FakeSource;
public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly string[] locations = new string[] { "Florence", "London", "New York", "Paris" };
    const ushort MAX_OUTSTANDING_CONFIRMS = 256;
    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var channelOpts = new CreateChannelOptions(
            publisherConfirmationsEnabled: true,
            publisherConfirmationTrackingEnabled: true,
            outstandingPublisherConfirmationsRateLimiter: new ThrottlingRateLimiter(MAX_OUTSTANDING_CONFIRMS)
        );
        Random random = new Random();
        var factory = new ConnectionFactory() { HostName = "localhost" };
        
        IConnection? connection = null;
        IChannel? channel = null;
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                try
                {
                    var purchaseDay = DateTime.UtcNow.Date;
                    //randomize a little bit purchase day
                    purchaseDay = purchaseDay.AddDays(random.Next(0, 3) - 1);

                    var purchase = new PurchaseMessage
                    {
                        //message time
                        PurchaseTime = Timestamp.FromDateTime(purchaseDay),
                        Time = Timestamp.FromDateTime(DateTime.UtcNow),
                        Id = Guid.NewGuid().ToString(),
                        //add random location
                        Location = locations[random.Next(0, locations.Length)],
                        //add random cost
                        Cost = 200 * random.Next(1, 4)
                    };
                    byte[]? body = null;
                    using (var stream = new MemoryStream())
                    {
                        purchase.WriteTo(stream);
                        stream.Flush();
                        body = stream.ToArray();
                    }
                    ResiliencePipeline pipeline = new ResiliencePipelineBuilder()
                        .AddRetry(new RetryStrategyOptions
                        {
                            ShouldHandle = new PredicateBuilder().Handle<Exception>(),
                            BackoffType = DelayBackoffType.Exponential,
                            UseJitter = true,  // Adds a random factor to the delay
                            MaxRetryAttempts = 4,
                            Delay = TimeSpan.FromSeconds(3),
                        })
                        .Build();

                    
                    await pipeline.ExecuteAsync(async (token) =>
                    {
                        try
                        {
                            if (connection == null || channel == null)
                            {
                                connection = await factory.CreateConnectionAsync();
                                channel = await connection.CreateChannelAsync(channelOpts);
                                
                            }

                            await channel.QueueDeclareAsync(queue: "purchase_queue",
                                durable: true,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);
                            var properties = new BasicProperties();
                            properties.Persistent = true;
                            await channel.BasicPublishAsync(exchange: "",
                                 routingKey: "purchase_queue",
                                 mandatory: true,
                                 basicProperties: properties,
                                 body: body); 
                        }
                        catch
                        {
                            if(channel != null)
                                channel.Dispose();
                            if(connection != null)
                                connection.Dispose();
                            channel = null;
                            connection = null;
                            throw;
                        }

                    }, stoppingToken);



                    await Task.Delay(2000, stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    return;
                }

            }
        }
        finally
        {
            if (connection != null)
            {
                if(channel != null)
                    channel.Dispose();
                connection.Dispose();
                channel = null;
                connection = null;
            }
        }
    }
}
