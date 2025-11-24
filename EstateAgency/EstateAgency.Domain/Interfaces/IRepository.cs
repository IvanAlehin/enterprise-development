namespace EstateAgency.Domain.Interfaces;

/// <summary>
/// Defines a generic repository interface for performing basic CRUD operations
/// on entities within a data storage system.
/// Provides asynchronous methods for retrieving, creating, updating, and deleting entities.
/// </summary>
/// <typeparam name="T">
/// The type of the entity managed by the repository.
/// </typeparam>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// Retrieves an entity by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity.</param>
    /// <returns>
    /// The result contains the entity if found; otherwise, <c>null</c>.
    /// </returns>
    public Task<T?> GetByIdAsync(int id);

    /// <summary>
    /// Retrieves all entities of type <typeparamref name="T"/>.
    /// </summary>
    /// <returns>
    /// The result contains a collection of all entities.
    /// </returns>
    public Task<IEnumerable<T>> GetAllAsync();

    /// <summary>
    /// Adds a new entity to the data storage.
    /// </summary>
    /// <param name="entity">The entity to be added.</param>
    /// <returns>
    /// The result contains the added entity instance.
    /// </returns>
    public Task<T> AddAsync(T entity);

    /// <summary>
    /// Updates an existing entity in the data storage.
    /// </summary>
    /// <param name="entity">The entity with updated data.</param>
    /// <returns>
    /// The result contains the updated entity instance.
    /// </returns>
    public Task<T> UpdateAsync(T entity);

    /// <summary>
    /// Deletes an entity by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity to delete.</param>
    /// <returns>
    /// The result is <c>true</c> if the entity was successfully deleted; otherwise, <c>false</c>.
    /// </returns>
    public Task<bool> DeleteAsync(int id);
}

