using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using RealtyMind.Api.Middleware;
using RealtyMind.Application;
using RealtyMind.Application.Configurations;
using RealtyMind.Infrastructure;
using RealtyMind.Infrastructure.Data;
using RealtyMind.Infrastructure.Data.Seed;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// retain existing app service registrations
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddHttpClient("GoogleMaps", client =>
{
    client.BaseAddress = new Uri("https://maps.googleapis.com/maps/api/");
});

builder.Services.AddHttpClient("RapidApiClient", client =>
{
    client.BaseAddress = new Uri("https://zillow56.p.rapidapi.com/");
});


builder.Services.Configure<GoogleConfig>(builder.Configuration.GetSection("Integrations:Google"));
builder.Services.Configure<RapidApiConfig>(builder.Configuration.GetSection("Integrations:RapidApi"));
builder.Services.Configure<MortgageConfig>(builder.Configuration.GetSection("Integrations:Mortgage"));

builder.Services.Configure<OverpassConfig>(builder.Configuration.GetSection("Integrations:Overpass"));
// Fix for CS8604: Ensure the configuration value is not null before using it to create a Uri.
builder.Services.AddHttpClient("OverpassClient", c =>
{
    var baseUrl = builder.Configuration["Integrations:Overpass:BaseUrl"];
    if (string.IsNullOrWhiteSpace(baseUrl))
    {
        throw new InvalidOperationException("Integrations:Overpass:BaseUrl configuration is missing or empty.");
    }
    c.BaseAddress = new Uri(baseUrl);
});
builder.Services.AddHttpClient("GooglePlaces", c => c.BaseAddress = new Uri("https://maps.googleapis.com/maps/api/place/"));

builder.Services.AddMemoryCache();

// JWT Authentication Setup
var jwtSection = builder.Configuration.GetSection("Jwt");
var jwtKey = jwtSection.GetValue<string>("Key");
var jwtIssuer = jwtSection.GetValue<string>("Issuer");
var jwtAudience = jwtSection.GetValue<string>("Audience");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        MarketIndexSeeder.Seed(db);
    }

}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<SimpleRateLimitMiddleware>();

// Map endpoints
app.MapGet("/health", () =>
{
    return Results.Ok(new
    {
        status = "ok",
        timestamp = DateTime.UtcNow
    });
});

app.MapGet("/ping", () =>
{
    return Results.Ok(new
    {
        message = "pong",
        timestamp = DateTime.UtcNow
    });
});

app.MapControllers();

app.Run();
