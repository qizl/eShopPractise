using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnjoyCodes.eShopOnWeb.ApplicationCore.Entities;
using EnjoyCodes.eShopOnWeb.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EnjoyCodes.eShopOnWeb.Infrastructure.Data
{
    public class EfRepository<T> : IRepository<T>, IAsyncRepository<T> where T : BaseEntity
    {
        protected readonly CatalogContext _dbContext;

        public EfRepository(CatalogContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public T Add(T entity)
        {
            this._dbContext.Set<T>().Add(entity);
            this._dbContext.SaveChanges();

            return entity;
        }

        public async Task<T> AddAsync(T entity)
        {
            this._dbContext.Set<T>().Add(entity);
            await this._dbContext.SaveChangesAsync();

            return entity;
        }

        public virtual T GetById(int id) => this._dbContext.Set<T>().Find(id);

        public async Task<T> GetByIdAsync(int id) => await this._dbContext.Set<T>().FindAsync(id);

        public T GetSingleBySpec(ISpecification<T> spec) => this.List(spec).FirstOrDefault();

        public IEnumerable<T> List(ISpecification<T> spec)
        {
            var queryableResultWithIncludes = spec.Includes.Aggregate(this._dbContext.Set<T>().AsQueryable(), (current, include) => current.Include(include));

            var secondaryResult = spec.IncludeStrings.Aggregate(queryableResultWithIncludes, (current, include) => current.Include(include));

            return secondaryResult.Where(spec.Criteria).AsEnumerable();
        }

        public async Task<List<T>> ListAsync(ISpecification<T> spec)
        {
            var queryableResultWithIncludes = spec.Includes.Aggregate(_dbContext.Set<T>().AsQueryable(), (current, include) => current.Include(include));

            var secondaryResult = spec.IncludeStrings.Aggregate(queryableResultWithIncludes, (current, include) => current.Include(include));

            return await secondaryResult.Where(spec.Criteria).ToListAsync();
        }

        public IEnumerable<T> ListAll() => this._dbContext.Set<T>().AsEnumerable();

        public async Task<List<T>> ListAllAsync() => await this._dbContext.Set<T>().ToListAsync();

        public void Update(T entity)
        {
            this._dbContext.Entry(entity).State = EntityState.Modified;
            this._dbContext.SaveChanges();
        }

        public async Task UpdateAsync(T entity)
        {
            this._dbContext.Entry(entity).State = EntityState.Modified;
            await this._dbContext.SaveChangesAsync();
        }

        public void Delete(T entity)
        {
            this._dbContext.Set<T>().Remove(entity);
            this._dbContext.SaveChanges();
        }

        public async Task DeleteAsync(T entity)
        {
            this._dbContext.Set<T>().Remove(entity);
            await this._dbContext.SaveChangesAsync();
        }
    }
}
