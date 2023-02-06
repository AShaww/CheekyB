using System.Linq.Expressions;

namespace CheekyData.Interfaces;

public interface IRepository<TEntity> where TEntity : class, new()
{
    Task<IEnumerable<TEntity>> GetAllAsync();

    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);

    Task<TEntity> AddAsync(TEntity entity);

    Task<TEntity> UpdateAsync(TEntity entity);

    Task<TEntity> DeleteAsync(TEntity entity);
    Task<TEntity> GetFirstOrDefault(Expression<Func<TEntity, bool>> expression);
    Task<bool> DoesExistInDb(Expression<Func<TEntity, bool>> predicate);
}
