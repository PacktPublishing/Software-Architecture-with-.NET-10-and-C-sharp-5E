using Microsoft.Net.Http.Headers;
using DBDriver.Extensions;
using DDD.ApplicationLayer;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.using DDD.ApplicationLayer;

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddCors(o => {
    o.AddDefaultPolicy(pbuilder =>
    {
        pbuilder.AllowAnyMethod();
        pbuilder.WithHeaders(HeaderNames.ContentType,
          HeaderNames.Authorization);
        pbuilder.WithOrigins("https://localhost:7257");
    });
});
builder.Services.AddDbDriver(
        builder.Configuration
         .GetConnectionString("DefaultConnection") ?? string.Empty);
builder.Services.AddApplicationServices();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
