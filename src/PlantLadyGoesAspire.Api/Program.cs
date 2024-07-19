using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/getPlants", () =>
{
    var plants = JsonSerializer.Deserialize<Plant[]>(System.IO.File.ReadAllText("plants.json")) ?? Array.Empty<Plant>();
    return plants;
})
.WithOpenApi();

app.Run();


internal record Plant(string Name, string? Image, string? Sunlight, string? Summary)
{
    public DateOnly LastWatered { get; } = DateOnly.FromDateTime(DateTime.Now.AddDays(-7));
}
