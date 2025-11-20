var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

builder.AddProject<Projects.CVP_Events>("webapp")
    .WithReference(cache)
    .WaitFor(cache);

builder.Build().Run();
