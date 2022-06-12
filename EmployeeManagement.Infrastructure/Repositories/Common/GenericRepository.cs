using EmployeeManagement.Domain.Interfaces.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManagement.Infrastructure.Repositories.Common
{
    public class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity>, IDisposable
        where TEntity : class
        where TContext : DbContext, new()
    {
        protected readonly TContext _context;
        private DbSet<TEntity> _set;
        public GenericRepository(TContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _set = _context.Set<TEntity>();
        }
        public virtual IQueryable<TEntity> GetAllQueryable(Expression<Func<TEntity, bool>> predicate = null)
        {
            var query = _context.Set<TEntity>();
            if (predicate != null)
                return query.Where(predicate);
            /*.AsNoTracking()*/
            return query;
        }
        public int Count(Expression<Func<TEntity, bool>> predicate = null)
        {
            var dataSet = _context.Set<TEntity>();
            if (predicate != null)
                return dataSet.Where(predicate).Count();
            return dataSet.Count();
        }
        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            var entities = new List<TEntity>();
            if (predicate != null)
                entities = _context.Set<TEntity>().Where(predicate).AsNoTracking().ToList();
            else
                entities = _context.Set<TEntity>().AsNoTracking().ToList();

            //_context.Entry(entities).State = EntityState.Detached;
            return entities;
        }
        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            var entities = new List<TEntity>();
            if (predicate != null)
                entities = await _context.Set<TEntity>().Where(predicate).ToListAsync();
            else
                entities = await _context.Set<TEntity>().ToListAsync();

            return entities;
        }
        public TEntity GetById(int id)
        {
            var entity = _context.Set<TEntity>().Find(id);
            _context.Entry(entity).State = EntityState.Detached;

            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            return entity;
        }
        public async Task<TEntity> GetByIdAsync(int id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            _context.Entry(entity).State = EntityState.Detached;

            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            return entity;
        }
        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate != null)
            {
                var entity = _context.Set<TEntity>().SingleOrDefault(predicate);
                _context.Entry(entity).State = EntityState.Detached;

                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));
                return entity;
            }
            return null;
        }
        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate != null)
            {
                var entity = await _context.Set<TEntity>().SingleOrDefaultAsync(predicate);
                if (entity == null)
                    return null;

                _context.Entry(entity).State = EntityState.Detached;
                return entity;
            }
            return null;
        }

        public TEntity Add(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return _context.Set<TEntity>().Add(entity).Entity;
        }
        public async Task AddAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await _context.Set<TEntity>().AddAsync(entity);
        }
        public void AddRange(List<TEntity> entities)
        {
            if (entities != null && entities.Count > 0)
                _context.Set<TEntity>().AddRange(entities);
        }
        public virtual async Task AddRangeAsync(List<TEntity> entities)
        {
            if (entities != null && entities.Count > 0)
                await _context.Set<TEntity>().AddRangeAsync(entities);
        }

        public void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.Set<TEntity>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }
        public async Task DeleteByIdAsync(int id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            _context.Set<TEntity>().Remove(entity);
        }
        public async Task DeleteWhere(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate != null)
                _context.Set<TEntity>().RemoveRange(await _context.Set<TEntity>().Where(predicate).ToListAsync());
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            // Dispatch Domain Events collection. 
            // Choices:
            // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
            // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
            // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
            // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
            // await _mediator.DispatchDomainEventsAsync(this);

            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed through the DbContext will be committed

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public void SaveEntities(CancellationToken cancellationToken = default(CancellationToken))
        {
            // Dispatch Domain Events collection. 
            // Choices:
            // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
            // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
            // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
            // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
            // await _mediator.DispatchDomainEventsAsync(this);

            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed through the DbContext will be committed
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public void Dispose()
        {
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
