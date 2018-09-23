using Autofac;
using EnjoyCodes.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
using EnjoyCodes.eShopOnContainers.Services.Ordering.API.Application.Commands;
using EnjoyCodes.eShopOnContainers.Services.Ordering.API.Application.Queries;
using EnjoyCodes.eShopOnContainers.Services.Ordering.Domain.AggregatesModel.BuyerAggregate;
using EnjoyCodes.eShopOnContainers.Services.Ordering.Domain.AggregatesModel.OrderAggregate;
using EnjoyCodes.eShopOnContainers.Services.Ordering.Infrastructure.Idempotency;
using EnjoyCodes.eShopOnContainers.Services.Ordering.Infrastructure.Repositories;
using System.Reflection;

namespace EnjoyCodes.eShopOnContainers.Services.Ordering.API.Infrastructure.AutofacModules
{

    public class ApplicationModule : Autofac.Module
    {

        public string QueriesConnectionString { get; }

        public ApplicationModule(string qconstr)
        {
            QueriesConnectionString = qconstr;

        }

        protected override void Load(ContainerBuilder builder)
        {

            builder.Register(c => new OrderQueries(QueriesConnectionString))
                .As<IOrderQueries>()
                .InstancePerLifetimeScope();

            builder.RegisterType<BuyerRepository>()
                .As<IBuyerRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<OrderRepository>()
                .As<IOrderRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<RequestManager>()
               .As<IRequestManager>()
               .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(CreateOrderCommandHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IIntegrationEventHandler<>));

        }
    }
}
