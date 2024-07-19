var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.PlantLadyGoesAspire_Blazor>("frontend");

builder.AddProject<Projects.PlantLadyGoesAspire_Api>("backend");

builder.Build().Run();
