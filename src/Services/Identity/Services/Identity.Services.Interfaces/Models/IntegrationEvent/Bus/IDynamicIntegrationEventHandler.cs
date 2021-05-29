using System.Threading.Tasks;

namespace Identity.Services.Interfaces.Models.IntegrationEvent.Bus
{
    public interface IDynamicIntegrationEventHandler
    {
        Task Handle(dynamic eventData);
    }
}
