using EventBus.Events;

namespace UniversityApi.IntegrationEvents;

public interface IUniversityIntegrationEventService
{

    Task test(IntegrationEvent @event);
}
