using RabbitMQ.Client;
using System;

namespace EnjoyCodes.eShopOnContainers.BuildingBlocks.EventBus.EventBusRabbitMQ
{
    public interface IRabbitMQPersistentConnection : IDisposable
    {
        bool IsConnected { get; }

        bool TryConnect();

        IModel CreateModel();
    }
}
