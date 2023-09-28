using JCS.Neon.Rest.Api;
using JCS.Neon.Rest.Api.Extensions;

// Create a web application builder
var builder = WebApplication.CreateBuilder(args);

// Configure logging based on Serilog for a better logging experience
builder.ConfigureLogging();

// bind our options
var apiOptions = new ApiOptions();
builder.Configuration.Bind("ApiOptions", apiOptions);

// Add and configure core authentication
builder.Services.AddCoreApiAuthentication(apiOptions);
builder.Services.AddCoreApiAuthorization(apiOptions);

// Add the core API configuration options
builder.Services.AddCoreApiConfiguration(builder.Configuration);

// Add the core API services
builder.Services.AddCoreApiServices();

// Configure the swagger definition
builder.Services.AddCoreApiSwaggerConfiguration();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsIntegration())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Redirect to Https
app.UseHttpsRedirection();

// Map the endpoints associated with the core api
app.MapCoreApiEndpoints();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .RequireAuthorization()
    .WithName("GetWeatherForecast")
    .WithOpenApi();

// Ensure that we enable authentication and authorisation
if (app.Environment.IsDevelopment())
{
}

app.UseAuthorization();

// Run ze app
app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}