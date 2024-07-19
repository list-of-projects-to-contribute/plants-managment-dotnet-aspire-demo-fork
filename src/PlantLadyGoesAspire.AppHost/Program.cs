using Aspire.Hosting;
using Aspire.Hosting.Azure;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Client.Extensions.Msal;

var builder = DistributedApplication.CreateBuilder(args);

var sqldb = builder.AddSqlServer("sqlserver").AddDatabase("sqldb");

var storage = builder.AddAzureStorage("storage");
var bobtheblob = storage.AddBlobs("bobtheblob");

if (builder.Environment.IsDevelopment())
{
    storage.RunAsEmulator(c => c.WithImageTag("3.30.0"));
}

var backend = builder.AddProject<Projects.PlantLadyGoesAspire_Api>("backend")
    .WithReference(bobtheblob).WithReference(sqldb);

builder.AddProject<Projects.PlantLadyGoesAspire_Blazor>("frontend")
    .WithReference(backend)
    .WithReference(bobtheblob);

builder.Build().Run();
