using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.OpenApi.Writers;
using System.Text.Json;

namespace PlantLadyGoesAspire.Api;
public class PlantStorageService(BlobServiceClient bob, IServiceScopeFactory ssf)
{
    public Plant[] GetAndStorePlants()
    {
        var container = bob.GetBlobContainerClient("plant-images");
        container.CreateIfNotExists(PublicAccessType.BlobContainer);
        var plants = GetPlants.GetPlantsFromJson();
        foreach (var plant in plants)
        {
            var blob = container.GetBlobClient(plant.Name);
            if (!blob.Exists())
            {
                blob.Upload(plant.Image);
            }
            plant.Image = blob.Uri.ToString();
        }
        var scope = ssf.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<NewDbContext>();
        db.Database.EnsureCreated();
        foreach (var p in plants)
            db.Plants.Add(p);
        db.SaveChanges();
        return plants;
    }

}
