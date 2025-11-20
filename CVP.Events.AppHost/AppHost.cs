var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.CVP_Events>("webapp");

builder.Build().Run();
