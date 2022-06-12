using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManagement.Domain.Interfaces.Repositories.Common
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAllQueryable(Expression<Func<TEntity, bool>> predicate = null);
        int Count(Expression<Func<TEntity, bool>> predicate = null);
        List<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null);
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null);
        TEntity GetById(int id);
        Task<TEntity> GetByIdAsync(int id);
        TEntity Get(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);

        TEntity Add(TEntity entity);
        Task AddAsync(TEntity entity);
        void AddRange(List<TEntity> entities);
        Task AddRangeAsync(List<TEntity> entities);

        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task DeleteByIdAsync(int id);
        Task DeleteWhere(Expression<Func<TEntity, bool>> predicate = null);

        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken));
        void SaveEntities(CancellationToken cancellationToken = default(CancellationToken));
    }
}
