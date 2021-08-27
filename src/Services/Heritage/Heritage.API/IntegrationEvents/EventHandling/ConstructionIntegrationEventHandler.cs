using Heritage.API.IntegrationEvents.Events;

namespace Heritage.API.IntegrationEvents.EventHandling
{
    public class ConstructionIntegrationEventHandler
    {
        public string LogInfo { get; set; }

        public ConstructionIntegrationEventHandler(ConstructionIntegrationEvent @event)
        {
            _ = @event.ConstructionName != null ?
             LogInfo = $"[{@event.DateTime}]-[{@event.Action}] {@event.ConstructionName} [{@event.ConstructionId}]"
             : LogInfo = $"[{@event.DateTime}]-[{@event.Action}]-[{@event.ConstructionId}]";
        }
    }
}
