using System.Linq.Expressions;
using CheekyData.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace CheekyData.Implementations;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
{
    protected readonly CheekyContext _cheekyContext;
    public Repository(CheekyContext cheekyContext)
    {
        _cheekyContext = cheekyContext ?? throw new ArgumentNullException(nameof(cheekyContext));
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
        => await _cheekyContext.Set<TEntity>().ToListAsync();

    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        => await _cheekyContext.Set<TEntity>().Where(predicate).ToListAsync();

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
        }

        try
        {
            await _cheekyContext.AddAsync(entity);
            await _cheekyContext.SaveChangesAsync();

            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception($"{nameof(entity)} could not be saved: {ex.Message}");
        }
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
        }

        try
        {
            _cheekyContext.Update(entity);
            await _cheekyContext.SaveChangesAsync();

            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception($"{nameof(entity)} could not be updated: {ex.Message}");
        }
    }

    public async Task<TEntity> DeleteAsync(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
        }

        try
        {
            _cheekyContext.Remove(entity);
            await _cheekyContext.SaveChangesAsync();

            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception($"{nameof(entity)} could not be deleted: {ex.Message}");
        }
    }

    public async Task<TEntity> GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        => await _cheekyContext.Set<TEntity>().FirstOrDefaultAsync(predicate);

    public async Task<bool> DoesExistInDb(Expression<Func<TEntity, bool>> predicate)
        => await _cheekyContext.Set<TEntity>().AnyAsync(predicate);
}