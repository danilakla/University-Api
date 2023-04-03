using EventBus.Abstructions;
using EventBus.Events;

namespace UniversityApi.IntegrationEvents;

public class UniversityIntegrationEventService: IUniversityIntegrationEventService
{
    private readonly IEventBus _eventBus;

    public UniversityIntegrationEventService(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public async Task CreateProfile(IntegrationEvent events)
    {
           _eventBus.Publish(events);
    }
}
