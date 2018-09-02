using System.Threading.Tasks;

namespace EnjoyCodes.eShopOnContainers.BuildingBlocks.EventBus.Abstractions
{
    public interface IDynamicIntegrationEventHandler
    {
        Task Handle(dynamic eventData);
    }
}
