using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.Text.Json;

namespace PlantLadyGoesAspire.Api;
public class PlantStorageService(BlobServiceClient bob)
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
        return plants;
    }

}
