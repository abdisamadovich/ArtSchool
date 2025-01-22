using System.Linq.Expressions;
using ArtSchools.App.Pagination.Base;
using ArtSchools.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace ArtSchools.Repositories.Base;

public interface IRepository<TEntity, TIdentifable> where TEntity : IIdentifiable<TIdentifable>
{
    Task<IQueryable<TEntity>> GetAllAsync();
    Task<IQueryable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression);
    Task<TEntity> GetAsync(TIdentifable id);
    Task<PagedResult<TEntity>> BrowseAsync<TQuery>(Expression<Func<TEntity, bool>> predicate,
        TQuery query) where TQuery : IPagedQuery;
    Task<PagedResult<TEntity>> BrowseAsync<TQuery>(Expression<Func<TEntity, bool>> predicate,
        TQuery query, params Expression<Func<TEntity, object>>[] includes) where TQuery : IPagedQuery;
    Task<PagedResult<TEntity>> BrowseAsync<TQuery>(Expression<Func<TEntity, bool>> predicate,
        TQuery query, params (Expression<Func<TEntity, object>> Include, Func<IIncludableQueryable<TEntity, object>, IIncludableQueryable<TEntity, object>> ThenInclude)[] includes) where TQuery : IPagedQuery;
    Task InsertAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TIdentifable id);
    Task DeleteAsync(Expression<Func<TEntity, bool>> expression);
    Task Save();
}