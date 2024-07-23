var builder = DistributedApplication.CreateBuilder(args);

var db_host = builder.AddPostgres("plantladygoesaspire-dbserver")
    .WithEnvironment("POSTGRES_DB", "PlantzDB")
    .WithPgAdmin();

var db = db_host.AddDatabase("PlantzDB");

var api = builder.AddProject<Projects.PlantLadyGoesAspire_Api>("plantladygoesaspire-api")
    .WithReference(db);

builder.AddProject<Projects.PlantLadyGoesAspire_Blazor>("plantladygoesaspire-blazor")
    .WithReference(api);

builder.Build().Run();
