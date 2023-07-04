using System.Linq.Expressions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using DDDMicroservice.Domain;
using DDDMicroservice.Domain.Entities;
using DDDMicroservice.Infrastructure.Mediator.Extensions;

namespace DDDMicroservice.Infrastructure.DataAccess.Repositories
{
    public class EFRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DatabaseContext _dbContext;
        private readonly IMediator _mediator;
        public EFRepository(DatabaseContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        protected virtual IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().Where<T>(predicate);
        }

        public virtual async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? predicate = null)
        {
            var query = _dbContext.Set<T>().Where<T>(x => !x.IsDeleted);
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return await query.ToListAsync();
        }

        public virtual async Task<T> Get(int id)
        {
            var entity = await _dbContext.Set<T>().FirstAsync(x => x.Id == id && !x.IsDeleted);
            return entity;
        }

        public virtual async Task<T>? Add(T entity)
        {
            var result = await _dbContext.Set<T>().AddAsync(entity);

            await Save();
            return entity;
        }

        public virtual async Task<T> Upsert(T entity)
        {
            var entityToUpdate = await _dbContext.Set<T>()
                                                .AsNoTracking()
                                                .SingleOrDefaultAsync<T>(x => x.Id == entity.Id && !x.IsDeleted);

            //Insert if entity is not found
            if (entityToUpdate == null)
            {
                return await Add(entity);
            }
            else //Update
            {
                _dbContext.Update(entity);
            }

            await Save();
            return entityToUpdate;
        }

        public async Task<bool> Delete(int id)
        {
            var entityToDelete = await _dbContext.Set<T>().SingleOrDefaultAsync(x => x.Id == id);

            if (entityToDelete == null) return false;

            //Hard Delete
            //_dbContext.Set<T>().Remove(entityToDelete);

            //Soft Delete
            entityToDelete.IsDeleted = true;
            entityToDelete.DeletedDateUtc = DateTime.UtcNow;

            await Save();
            return true;
        }

        public async Task<bool> Delete(IEnumerable<int> ids)
        {
            var entitiesToDelete = _dbContext.Set<T>().Where(x => ids.Contains(x.Id));

            if (!entitiesToDelete.Any()) return false;

            //Hard Delete
            //_dbContext.Set<T>().RemoveRange(entitiesToDelete);

            //Soft Delete
            await entitiesToDelete.ForEachAsync(e =>
            {
                e.IsDeleted = true;
                e.DeletedDateUtc = DateTime.UtcNow;
            });

            await Save();
            return true;
        }

        protected async Task<int> Save(bool cleanTracker = true)
        {
            // Dispatch Domain Events collection. 
            // Choices:
            // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
            // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
            // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
            // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers.
            await _mediator.DispatchDomainEventsAsync(_dbContext);

            var result = await _dbContext.SaveChangesAsync();

            if (cleanTracker)
            {
                _dbContext.ChangeTracker.Clear();
            }

            return result;
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string? ToString()
        {
            return base.ToString();
        }
    }
}