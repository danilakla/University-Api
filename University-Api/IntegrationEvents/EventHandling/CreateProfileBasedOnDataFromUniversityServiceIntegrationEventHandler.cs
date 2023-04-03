using EventBus.Abstructions;
using UniversityApi.IntegrationEvents.Events;

namespace UniversityApi.IntegrationEvents.EventHandling;

public class CreateProfileBasedOnDataFromUniversityServiceIntegrationEventHandler :
        IIntegrationEventHandler<CreateProfileBaseOnUniverDataIntegrationEvent>

{
    public Task Handle(CreateProfileBaseOnUniverDataIntegrationEvent @event)
    {
        throw new NotImplementedException();
    }
}
