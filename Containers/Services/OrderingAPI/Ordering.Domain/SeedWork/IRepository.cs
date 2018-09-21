﻿namespace EnjoyCodes.eShopOnContainers.Services.Ordering.Domain.SeedWork
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
