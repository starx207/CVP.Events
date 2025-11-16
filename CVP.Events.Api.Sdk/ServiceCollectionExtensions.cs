using CVP.Events.Api.Sdk.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Refit;

namespace CVP.Events.Api.Sdk;

public static class ServiceCollectionExtensions
{
    private static readonly RefitSettings _refitSettings = new()
    {
        UrlParameterKeyFormatter = new DollarPrefixFormatter()
    };

    extension(IServiceCollection services)
    {
        public IServiceCollection AddEventsApiClient(Action<EventsApiOptions>? configureOptions = null)
        {
            services.AddOptions<EventsApiOptions>().Configure(opt =>
            {
                configureOptions?.Invoke(opt);
                opt.BaseUrl ??= "https://interview.civicplus.com/634d4763-284b-4ea0-ba02-40adb38a5c32";
            });

            services
                .AddSingleton<AuthTokenProvider>()
                .AddTransient<AuthHeaderHandler>();

            services.AddRefitClient<IAuthApi>(_refitSettings)
                .ConfigureHttpClient(ConfigureBaseApiUrl);

            services.AddRefitClient<IEventsApi>(_refitSettings)
                .ConfigureHttpClient(ConfigureBaseApiUrl)
                .AddHttpMessageHandler<AuthHeaderHandler>();

            return services;
        }
    }

    private static void ConfigureBaseApiUrl(IServiceProvider provider, HttpClient client)
        => client.BaseAddress = new Uri(provider.GetRequiredService<IOptions<EventsApiOptions>>().Value.BaseUrl!);
}
