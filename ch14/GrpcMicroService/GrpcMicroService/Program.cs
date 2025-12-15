using DBDriver.Extensions;
using DDD.ApplicationLayer;
using Google.Protobuf.WellKnownTypes;
using GrpcMicroService.HostedServices;
using GrpcMicroService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddApplicationServices();
builder.Services.AddDbDriver(
  builder.Configuration.
    GetConnectionString("DefaultConnection")??string.Empty);
builder.Services.AddHostedService<ProcessPurchases>();
var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<CounterService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
