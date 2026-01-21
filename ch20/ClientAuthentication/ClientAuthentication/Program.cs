using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<KestrelServerOptions>(options =>
{
    if (builder.Environment.IsProduction())
        options.ConfigureHttpsDefaults(options =>
            options.ClientCertificateMode = ClientCertificateMode.RequireCertificate);
    else
        options.ConfigureHttpsDefaults(options =>
            options.ClientCertificateMode = ClientCertificateMode.AllowCertificate);
});
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(7079, options =>
    {

        options.UseHttps(Path.Combine(builder.Environment.ContentRootPath, "root_cc.pfx"), "_uhjasd");

    });
});
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddAuthentication(
        CertificateAuthenticationDefaults.AuthenticationScheme)
    .AddCertificate(options =>
    {
        options.AllowedCertificateTypes = CertificateTypes.All;
        options.Events = new CertificateAuthenticationEvents
        {
            
            OnCertificateValidated = context =>
            {
                if (context.ClientCertificate.Issuer!= "CN=localhost")
                {
                    context.Fail("Uniknown Client");
                    return Task.CompletedTask;
                }
                var attributes=context.ClientCertificate.SubjectName
                 .EnumerateRelativeDistinguishedNames()
                 .Where(m => m.GetSingleElementType()?.FriendlyName is not null)
                 .ToDictionary(
                    m => m.GetSingleElementType().FriendlyName!,
                    m => m.GetSingleElementValue());
                var name = attributes["CN"]??string.Empty;
                var claims = new[]
               {
                    new Claim(
                        ClaimTypes.NameIdentifier,
                        name,
                        ClaimValueTypes.String, context.Options.ClaimsIssuer),
                    new Claim(
                        ClaimTypes.Name,
                        name,
                        ClaimValueTypes.String, context.Options.ClaimsIssuer)
                };

                context.Principal = new ClaimsPrincipal(
                    new ClaimsIdentity(claims, context.Scheme.Name));
                context.Success();

                return Task.CompletedTask;
            }


        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseAuthentication();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
