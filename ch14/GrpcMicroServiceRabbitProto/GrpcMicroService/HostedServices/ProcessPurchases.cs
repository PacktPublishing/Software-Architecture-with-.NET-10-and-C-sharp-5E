using Azure.Core;
using DDD.ApplicationLayer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using WorkerMApplicationServices.Commands;
using WorkerMDomainServices.DTOs;

namespace GrpcMicroService.HostedServices;
public class ProcessPurchases : BackgroundService
{
    IServiceProvider services;
    const ushort MAX_OUTSTANDING_CONFIRMS = 256;
    public ProcessPurchases(IServiceProvider services)
    {
        this.services = services;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var channelOpts = new CreateChannelOptions(
            publisherConfirmationsEnabled: true,
            publisherConfirmationTrackingEnabled: true,
            outstandingPublisherConfirmationsRateLimiter: new ThrottlingRateLimiter(MAX_OUTSTANDING_CONFIRMS)
        );

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = await factory.CreateConnectionAsync())
                using (var channel = await connection.CreateChannelAsync(channelOpts))
                {
                    await channel.QueueDeclareAsync(queue: "purchase_queue",
                                         durable: true,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);
                    await channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false);

                    var consumer = new AsyncEventingBasicConsumer(channel);
                    consumer.ReceivedAsync += async (sender, ea) =>
                    {
                        if (stoppingToken.IsCancellationRequested)
                        {
                            channel.Dispose();
                            connection.Dispose();
                            return;
                        }
                        using (var scope = services.CreateScope())
                        {
                            try
                            {
                                var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<PurchaseCommand>>();
                                var body = ea.Body.ToArray();
                                PurchaseMessage? message = null;
                                using (var stream = new MemoryStream(body))
                                {
                                    message = PurchaseMessage.Parser.ParseFrom(stream);
                                }
                                bool success = false;
                                try
                                {
                                    await handler.HandleAsync(new PurchaseCommand(new PurchaseInfoDTO
                                    {
                                        Cost = message.Cost,
                                        MessageId = Guid.Parse(message.Id),
                                        Location = message.Location,
                                        PurchaseTime = message.PurchaseTime.ToDateTimeOffset(),
                                        Time = message.Time.ToDateTimeOffset()
                                    }));
                                    success=true;
                                }
                                catch
                                {
                                    success = false;
                                }


                                // Note: it is possible to access the channel via
                                //       ((AsyncEventingBasicConsumer)sender).Channel here
                                if (success)
                                    await ((AsyncEventingBasicConsumer)sender).Channel
                                        .BasicAckAsync(ea.DeliveryTag, false);
                                else
                                    await ((AsyncEventingBasicConsumer)sender).Channel
                                        .BasicNackAsync(ea.DeliveryTag, false, true);
                            }
                            catch {
                                try
                                {
                                    await ((AsyncEventingBasicConsumer)sender).Channel.BasicNackAsync(ea.DeliveryTag, false, true);
                                }
                                catch { }
                            }
                        }
                    };

                    await channel.BasicConsumeAsync(queue: "purchase_queue",
                                autoAck: false,
                                consumer: consumer, cancellationToken: stoppingToken);
                }
             }
            catch { }
        }
    }
}
