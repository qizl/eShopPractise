﻿using System.Collections.Generic;
using System.Threading.Tasks;
using EnjoyCodes.eShopOnWeb.ApplicationCore.Entities;

namespace EnjoyCodes.eShopOnWeb.ApplicationCore.Interfaces
{
    public interface IAsyncRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<List<T>> ListAllAsync();
        Task<List<T>> ListAsync(ISpecification<T> spec);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
