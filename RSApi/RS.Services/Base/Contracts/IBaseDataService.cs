using System.Collections.Generic;
using RS.Domain.Models;

namespace RS.Services.Base.Contracts
{
    /// <summary>
    /// The base data service.
    /// </summary>
    public interface IBaseDataService<T> where T : BaseDbModel
    {
        /// <summary>
        /// Creates the specified entity to insert.
        /// </summary>
        /// <param name="entityToInsert">The entity to insert.</param>
        /// <returns></returns>
        int? Create(T entityToInsert);
        
        /// <summary>
        /// Creates the specified entity to insert.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="entityToInsert">The entity to insert.</param>
        /// <returns></returns>
        TKey Create<TKey>(object entityToInsert);
        
        /// <summary>
        /// Creates the specified entity to insert.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entityToInsert">The entity to insert.</param>
        /// <returns></returns>
        TKey Create<TKey, TEntity>(TEntity entityToInsert);
        
        /// <summary>
        /// Updates the specified entity to insert.
        /// </summary>
        /// <param name="entityToInsert">The entity to insert.</param>
        void Update(T entityToInsert);
        
        /// <summary>
        /// Deletes the specified entity to insert.
        /// </summary>
        /// <param name="entityToInsert">The entity to insert.</param>
        void Delete(T entityToInsert);
        
        /// <summary>
        /// Gets the by.
        /// </summary>
        /// <param name="whereContitions">The where contitions.</param>
        /// <returns></returns>
        T GetBy(object whereContitions);
        
        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="whereContitions">The where contitions.</param>
        /// <returns></returns>
        IEnumerable<T> GetList(object whereContitions);

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetList();
    }
}