// Create the web application builder with command-line arguments
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Register Razor Pages services for page-based web applications
builder.Services.AddRazorPages();

// Configure HTTP Strict Transport Security (HSTS) options
// Preload: Enable HSTS preload list submission
// IncludeSubDomains: Apply HSTS to all subdomains
// MaxAge: Browser should remember HSTS policy for 60 days
builder.Services.AddHsts(options =>
{
    options.Preload = true;
    options.IncludeSubDomains = true;
    options.MaxAge = TimeSpan.FromDays(60);
});

// Build the web application with configured services
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // Redirect unhandled exceptions to the /Error page in production
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    // Enable HSTS middleware to enforce secure connections
    app.UseHsts();
}
else
    // Show detailed error pages during development for easier debugging
    app.UseDeveloperExceptionPage();

// Redirect HTTP requests to HTTPS for secure communication
app.UseHttpsRedirection();

// Enable endpoint routing to match incoming requests to endpoints
app.UseRouting();

// Enable authorization middleware to check user permissions
app.UseAuthorization();

// Map static asset endpoints for optimized static file serving
app.MapStaticAssets();
// Map Razor Pages endpoints with static asset optimization
app.MapRazorPages()
   .WithStaticAssets();

// Start the web application and begin listening for requests
app.Run();
