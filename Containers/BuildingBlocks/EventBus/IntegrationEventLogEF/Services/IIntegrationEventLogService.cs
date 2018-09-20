using EnjoyCodes.eShopOnContainers.BuildingBlocks.EventBus.Events;
using System.Data.Common;
using System.Threading.Tasks;

namespace EnjoyCodes.eShopOnContainers.BuildingBlocks.IntegrationEventLogEF.Services
{
    public interface IIntegrationEventLogService
    {
        Task SaveEventAsync(IntegrationEvent @event, DbTransaction transaction);
        Task MarkEventAsPublishedAsync(IntegrationEvent @event);
    }
}
