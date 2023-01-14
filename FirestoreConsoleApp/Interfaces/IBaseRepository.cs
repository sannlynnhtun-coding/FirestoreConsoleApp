using Google.Cloud.Firestore;

namespace Firestore.Interfaces;

/// <summary>
/// Represents a firestore base repository.
/// </summary>
public interface IBaseRepository<T>
{
    /// <summary>
    /// Gets all record from the repository.
    /// </summary> 
    /// <returns>a records of type T</returns>
    Task<List<T>> GetAllAsync<T>() where T : IBaseFirestoreEntity;

    /// <summary>
    /// Gets a record from the repository.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>a record of type T</returns>
    Task<object> GetAsync<T>(T entity) where T : IBaseFirestoreEntity;

    /// <summary>
    /// Adds a record to the repository.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>a record of type T</returns>
    Task<T> AddAsync<T>(T entity) where T : IBaseFirestoreEntity;

    /// <summary>
    /// Updates a record in the repository.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>a record of type T</returns>
    Task<T> UpdateAsync<T>(T entity) where T : IBaseFirestoreEntity;

    /// <summary>
    /// Adds a record to the repository.
    /// </summary>
    /// <param name="entity"></param> S
    Task DeleteAsync<T>(T entity) where T : IBaseFirestoreEntity;

    /// <summary>
    /// Query all record from the repository.
    /// </summary> 
    /// <returns>a records of type T</returns>
    Task<List<T>> QueryRecordsAsync<T>(Query query) where T : IBaseFirestoreEntity;
}
