using EstateAgency.Domain.Interfaces;
using EstateAgency.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EstateAgency.Infrastructure.Repositories;

/// <summary>
/// Generic repository implementation for performing basic CRUD operations using Entity Framework Core.
/// </summary>
/// <typeparam name="T">The entity type.</typeparam>
public class DbRepository<T>(AppDbContext context) : IRepository<T> where T : class
{
    /// <summary>
    /// The Entity Framework DbSet for the entity type.
    /// </summary>
    protected readonly DbSet<T> _set = context.Set<T>();

    /// <summary>
    /// Retrieves an entity by its ID.
    /// </summary>
    /// <param name="id">The entity ID.</param>
    /// <returns>The entity if found; otherwise, null.</returns>
    public async Task<T?> GetByIdAsync(int id)
    {
        return await _set.FindAsync(id);
    }

    /// <summary>
    /// Retrieves all entities of type <typeparamref name="T"/>.
    /// </summary>
    /// <returns>A collection of entities.</returns>
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _set.ToListAsync();
    }

    /// <summary>
    /// Adds a new entity to the database.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <returns>The added entity.</returns>
    public async Task<T> AddAsync(T entity)
    {
        await _set.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Updates an existing entity.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <returns>The updated entity.</returns>
    public async Task<T> UpdateAsync(T entity)
    {
        _set.Update(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Deletes an entity by its ID.
    /// </summary>
    /// <param name="id">The entity ID.</param>
    /// <returns>True if deleted; otherwise, false.</returns>
    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _set.FindAsync(id);
        if (entity == null)
            return false;

        _set.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }
}
