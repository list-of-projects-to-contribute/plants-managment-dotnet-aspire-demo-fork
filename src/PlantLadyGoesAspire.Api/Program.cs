using Azure.Storage.Blobs;
using PlantLadyGoesAspire.Api;
using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.AddAzureBlobClient("bobtheblob");
builder.Services.AddSingleton<PlantStorageService>();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var plantStorageService = app.Services.GetRequiredService<PlantStorageService>();

app.MapGet("/getPlants", () =>
{
    return plantStorageService.GetAndStorePlants();
})
.WithOpenApi();

app.Run();


public record Plant(string Name, string? Sunlight, string? Summary)
{
    public DateOnly LastWatered { get; } = DateOnly.FromDateTime(DateTime.Now.AddDays(-7));
    public string? Image { get; set; }
}