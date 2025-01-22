using System.Linq.Expressions;
using ArtSchools.App.Pagination.Base;
using ArtSchools.Entities;
using ArtSchools.Entities.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace ArtSchools.Repositories.Base;

internal class Repository<TEntity, TIdentifiable> : IRepository<TEntity, TIdentifiable>
        where TEntity : class, IIdentifiable<TIdentifiable>
    {
        private readonly DbContext _dbContext;

        public Repository(IApplicationDbContext dbContext)
            => _dbContext = dbContext.Context ?? throw new ArgumentNullException(nameof(dbContext));

        public async Task<IQueryable<TEntity>> GetAllAsync()
            => await Task.FromResult(_dbContext.Set<TEntity>().AsNoTracking());

        public async Task<IQueryable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression)
            =>  await Task.FromResult(_dbContext.Set<TEntity>().Where(expression).AsNoTracking());

        public async Task<TEntity> GetAsync(TIdentifiable id)
            => await _dbContext.Set<TEntity>()
                .AsNoTracking()
                .SingleOrDefaultAsync(e => e.Id.Equals(id));

        public async Task<PagedResult<TEntity>> BrowseAsync<TQuery>(Expression<Func<TEntity, bool>> predicate,
            TQuery query) where TQuery : IPagedQuery
            => await _dbContext.Set<TEntity>().Where(predicate).AsNoTracking().PaginateAsync(query);
        
        public async Task<PagedResult<TEntity>> BrowseAsync<TQuery>(Expression<Func<TEntity, bool>> predicate,
            TQuery query, params Expression<Func<TEntity, object>>[] includes) where TQuery : IPagedQuery
        {
            IQueryable<TEntity> queryable = _dbContext.Set<TEntity>().Where(predicate);
            foreach (var include in includes)
            {
                queryable = queryable.Include(include);
            }
            return await _dbContext.Set<TEntity>().Where(predicate).AsNoTracking().PaginateAsync(query);
        }
        
        public async Task<PagedResult<TEntity>> BrowseAsync<TQuery>(Expression<Func<TEntity, bool>> predicate,
            TQuery query, params (Expression<Func<TEntity, object>> Include, Func<IIncludableQueryable<TEntity, object>, IIncludableQueryable<TEntity, object>> ThenInclude)[] includes) where TQuery : IPagedQuery
        {
            IQueryable<TEntity> queryable = _dbContext.Set<TEntity>().Where(predicate);
           
            foreach (var (include, thenInclude) in includes)
            {
                var includableQueryable = queryable.Include(include);
                if (thenInclude != null)
                {
                    queryable = thenInclude(includableQueryable);
                }
                else
                {
                    queryable = includableQueryable;
                }
            }
            
            return await _dbContext.Set<TEntity>().Where(predicate).AsNoTracking().PaginateAsync(query);
        }

        public async Task InsertAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await Save();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            await Save();
        }

        public async Task DeleteAsync(TIdentifiable id)
        {
            var entityToDelete = await GetAsync(id);
            _dbContext.Set<TEntity>().Remove(entityToDelete);
            await Save();
        }

        public async Task DeleteAsync(Expression<Func<TEntity, bool>> expression)
        {
            var entitiesToDelete = await _dbContext.Set<TEntity>().Where(expression).ToListAsync();
            if (entitiesToDelete.Any())
            {
                _dbContext.Set<TEntity>().RemoveRange(entitiesToDelete);
                await Save();
            }
        }

        public async Task Save()
            => await _dbContext.SaveChangesAsync();
    }