using EnjoyCodes.eShopOnContainers.BuildingBlocks.EventBus.Events;
using System.Threading.Tasks;

namespace EnjoyCodes.eShopOnContainers.Services.Ordering.API.Application.IntegrationEvents
{
    public interface IOrderingIntegrationEventService
    {
        Task PublishThroughEventBusAsync(IntegrationEvent evt);
    }
}
