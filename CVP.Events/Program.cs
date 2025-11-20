using CVP.Events.Components;
using CVP.Events.Api.Sdk;
using CVP.Events.Services;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.AddRedisDistributedCache("cache");

builder.Services.AddEventsApiClient(options =>
{
    options.ClientId = builder.Configuration.GetValue<string>("EventsApi:ClientId");
    options.ClientSecret = builder.Configuration.GetValue<string>("EventsApi:ClientSecret");
});

builder.Services.AddTransient<CachedEventsApi>();
builder.Services.Decorate<IEventsApi, CachedEventsApi>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
