using EnjoyCodes.eShopOnContainers.BuildingBlocks.EventBus.Events;
using System.Threading.Tasks;

namespace EnjoyCodes.eShopOnContainers.Services.CatalogAPI.IntegrationEvents
{
    public interface ICatalogIntegrationEventService
    {
        Task SaveEventAndCatalogContextChangesAsync(IntegrationEvent evt);
        Task PublishThroughEventBusAsync(IntegrationEvent evt);
    }
}
