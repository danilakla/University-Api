using EventBus.Events;

namespace UniversityApi.IntegrationEvents;

public interface IUniversityIntegrationEventService
{

    Task CreateProfile(IntegrationEvent @event);



}
