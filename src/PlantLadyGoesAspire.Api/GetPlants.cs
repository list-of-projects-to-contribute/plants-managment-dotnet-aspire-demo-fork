using System.Text.Json;

namespace PlantLadyGoesAspire.Api
{
    public static class GetPlants
    {

        public static Plant[] GetPlantsFromJson()
        {
            var plants = JsonSerializer.Deserialize<Plant[]>(System.IO.File.ReadAllText("plants.json")) ?? Array.Empty<Plant>();
            return plants;
        }
    }
}
