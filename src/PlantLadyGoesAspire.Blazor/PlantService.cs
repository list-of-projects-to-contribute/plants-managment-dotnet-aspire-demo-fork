using Azure.Storage.Blobs;
using PlantLadyGoesAspire.Blazor;
using static System.Net.WebRequestMethods;

namespace PlantLadyGoesAspire.Blazor
{
    public class PlantService(HttpClient hc, BlobServiceClient bob)
    {
        public async Task<Plant[]> GetPlantsAsync(CancellationToken cancellationToken = default)
        {
            Plant[]? plants = null;
            plants = await hc.GetFromJsonAsync<Plant[]>("/getPlants", cancellationToken);
            return plants;

        }


    }
}

public record class Plant(string Name, string? Sunlight, string? Summary, string? Image)
{
    public DateOnly LastWatered { get; set; } 
}