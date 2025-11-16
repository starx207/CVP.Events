using System;
using System.ComponentModel;
using CVP.Events.Api.Sdk.Requests;
using CVP.Events.Contracts.Responses;
using Refit;

namespace CVP.Events.Api.Sdk;

public interface IEventsApi
{
    Task<AllEventsResponse> GetEventsAsync(GetAllEventsRequest request, CancellationToken cancellationToken = default)
        => GetEventsAsync(EventSearchRequest.FromEventRequest(request), cancellationToken);

    [Get("/api/events")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    Task<AllEventsResponse> GetEventsAsync(EventSearchRequest request, CancellationToken cancellationToken = default);

    [Get("/api/events/{id}")]
    Task<EventDetailsResponse> GetEventByIdAsync(string id, CancellationToken cancellationToken = default);

    [Post("/api/events")]
    Task<EventCreatedResponse> CreateEventAsync(CreateEventRequest request, CancellationToken cancellationToken = default);
}
