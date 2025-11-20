using System;
using System.Text.Json;
using CVP.Events.Api.Sdk;
using CVP.Events.Api.Sdk.Requests;
using CVP.Events.Contracts.Responses;
using Microsoft.Extensions.Caching.Distributed;

namespace CVP.Events.Services;

public class CachedEventsApi : IEventsApi
{
    private readonly IEventsApi _innerApi;
    private readonly IDistributedCache _cache;
    private readonly ILogger<CachedEventsApi> _logger;
    private readonly string _cachedSearchesKey = "CachedSearchKeys";

    public CachedEventsApi(IEventsApi innerApi, IDistributedCache cache, ILogger<CachedEventsApi> logger)
    {
        _innerApi = innerApi;
        _cache = cache;
        _logger = logger;
    }

    public async Task<EventCreatedResponse> CreateEventAsync(CreateEventRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _innerApi.CreateEventAsync(request, cancellationToken);
        await RemoveCachedSearchesAsync(cancellationToken);
        return response;
    }

    public Task<EventDetailsResponse> GetEventByIdAsync(string id, CancellationToken cancellationToken = default)
        => GetFromCacheOrApiAsync($"EventDetail:{id}", () => _innerApi.GetEventByIdAsync(id, cancellationToken), cancellationToken);

    public Task<AllEventsResponse> GetEventsAsync(EventSearchRequest request, CancellationToken cancellationToken = default)
    {
        var searchKey = JsonSerializer.Serialize(request);
        return GetFromCacheOrApiAsync($"EventSearch:{searchKey}", async () =>
        {
            var response = await _innerApi.GetEventsAsync(request, cancellationToken);
            await UpdateCachedSearchKeysAsync(searchKey, cancellationToken);
            return response;
        }, cancellationToken);
    }

    private async Task<T> GetFromCacheOrApiAsync<T>(string key, Func<Task<T>> apiCall, CancellationToken cancellationToken) where T : class
    {
        var cachedItem = await _cache.GetAsync(key, cancellationToken);
        if (cachedItem is not null)
        {
            var deserialized = JsonSerializer.Deserialize<T>(cachedItem);
            if (deserialized is not null)
            {
                _logger.LogDebug("Found cached item for key ({key}). Returning cached item", key);
                return deserialized;
            }
        }

        _logger.LogDebug("No cached item found for key ({key}). Fetching from API and caching item.", key);
        var response = await apiCall();
        await _cache.SetStringAsync(key, JsonSerializer.Serialize(response), cancellationToken);
        return response;
    }

    private async Task UpdateCachedSearchKeysAsync(string searchKey, CancellationToken cancellationToken)
    {
        var cachedSearchKeys = await _cache.GetAsync(_cachedSearchesKey, cancellationToken);
        List<string> cachedKeys = [];
        if (cachedSearchKeys is not null)
        {
            var deserialized = JsonSerializer.Deserialize<string[]>(cachedSearchKeys);
            if (deserialized is not null)
            {
                cachedKeys.AddRange(deserialized);
            }
        }

        if (!cachedKeys.Contains(searchKey))
        {
            _logger.LogDebug("Adding search key to list of cached searches: {key}", searchKey);
            cachedKeys.Add(searchKey);
            await _cache.SetStringAsync(_cachedSearchesKey, JsonSerializer.Serialize(cachedKeys));
        }
    }

    private async Task RemoveCachedSearchesAsync(CancellationToken cancellationToken)
    {
        _logger.LogDebug("Removing cached searches...");
        if (await _cache.GetAsync(_cachedSearchesKey, cancellationToken) is not { } cachedSearches)
        {
            return;
        }
        if (JsonSerializer.Deserialize<string[]>(cachedSearches) is not { } deserializedSearches)
        {
            return;
        }

        _logger.LogDebug("Found {count} cached searches to remove", deserializedSearches.Length);
        foreach (var searchKey in deserializedSearches)
        {
            await _cache.RemoveAsync($"EventSearch:{searchKey}", cancellationToken);
        }
    }
}
