using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace PokeTuberToolkit.Data.Util;

public static class DbSetExtensions
{
    public static TEntity GetOrCreate<TEntity>(
        this DbSet<TEntity> dbSet,
        Expression<Func<TEntity, bool>> predicate,
        Func<TEntity> createFunc)
        where TEntity : class
    {
        var existingEntity = dbSet.FirstOrDefault(predicate);
        if (existingEntity != null)
        {
            return existingEntity;
        }

        var newEntity = createFunc();
        dbSet.Add(newEntity);
        return newEntity;
    }

    public static async Task<TEntity> GetOrCreateAsync<TEntity>(
        this DbSet<TEntity> dbSet,
        Expression<Func<TEntity, bool>> predicate,
        Func<TEntity> createFunc,
        CancellationToken cancellationToken = default)
        where TEntity : class
    {
        var existingEntity = await dbSet.FirstOrDefaultAsync(predicate, cancellationToken);
        if (existingEntity != null)
        {
            return existingEntity;
        }

        var newEntity = createFunc();
        dbSet.Add(newEntity);
        return newEntity;
    }
}