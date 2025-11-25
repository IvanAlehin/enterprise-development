var builder = DistributedApplication.CreateBuilder(args);

var mysql = builder.AddMySql("MySql")
    .AddDatabase("EstateAgencyDb");

builder.AddProject<Projects.EstateAgency_Api>("EstateAgencytApi")
    .WithReference(mysql, "DefaultConnection")
    .WaitFor(mysql);

builder.Build().Run();
