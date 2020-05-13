using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RS.Domain.Models;

namespace RS.Repositories.Contracts
{
    /// <summary>
    /// The repository.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDbRepository<T> : IDapperRepository where T : BaseDbModel
    {
        #region Get

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        T Get(object id);

        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<T> GetAsync(object id);

        #endregion

        #region Delete

        /// <summary>
        /// Deletes the specified entity to delete.
        /// </summary>
        /// <param name="entityToDelete">The entity to delete.</param>
        /// <returns></returns>
        int Delete(T entityToDelete);

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        int Delete(object id);

        /// <summary>
        /// Deletes the list.
        /// </summary>
        /// <param name="entitiesIds">The entities identifier.</param>
        /// <returns></returns>
        int DeleteList(IList<int> entitiesIds);

        /// <summary>
        /// Deletes the list.
        /// </summary>
        /// <param name="whereConditions">The where conditions.</param>
        /// <returns></returns>
        int DeleteList(object whereConditions);

        /// <summary>
        /// Deletes the list.
        /// </summary>
        /// <param name="conditions">The conditions.</param>
        /// <returns></returns>
        int DeleteList(string conditions);

        /// <summary>
        /// Deletes the list asynchronous.
        /// </summary>
        /// <param name="entitiesIds">The entities ids.</param>
        /// <returns></returns>
        Task<int> DeleteListAsync(IList<int> entitiesIds);

        /// <summary>
        /// Deletes the list asynchronous.
        /// </summary>
        /// <param name="conditions">The conditions.</param>
        /// <returns></returns>
        Task<int> DeleteListAsync(string conditions);

        /// <summary>
        /// Deletes the list asynchronous.
        /// </summary>
        /// <param name="whereConditions">The where conditions.</param>
        /// <returns></returns>
        Task<int> DeleteListAsync(object whereConditions);

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="entityToDelete">The entity to delete.</param>
        /// <returns></returns>
        Task<int> DeleteAsync(T entityToDelete);

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<int> DeleteAsync(object id);

        /// <summary>
        /// Deletes the list asynchronous.
        /// </summary>
        /// <param name="entitiesToDelete">The entities to delete.</param>
        /// <returns></returns>
        Task DeleteListAsync(List<T> entitiesToDelete);

        #endregion

        #region Query

        /// <summary>
        /// Queries the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        IEnumerable<T> Query(string query, object param);

        /// <summary>
        /// Queries the specified SQL.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="map">The map.</param>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        IEnumerable<T> Query(string sql, Func<T, T, T> map, object param = null);

        /// <summary>
        /// Queries the specified SQL.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="map">The map.</param>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        IEnumerable<T> Query(string sql, Func<T, T, T, T> map, object param = null);

        /// <summary>
        /// Queries the specified SQL.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <param name="sql">The SQL.</param>
        /// <param name="map">The map.</param>
        /// <param name="splitOn">The split on.</param>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        IEnumerable<T> Query<T1, T2>(string sql, Func<T1, T2, T> map, string splitOn = "", object param = null);

        /// <summary>
        /// Queries the specified SQL.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <typeparam name="T3">The type of the 3.</typeparam>
        /// <param name="sql">The SQL.</param>
        /// <param name="map">The map.</param>
        /// <param name="splitOn">The split on.</param>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        IEnumerable<T> Query<T1, T2, T3>(string sql, Func<T1, T2, T3, T> map, string splitOn = "", object param = null);

        /// <summary>
        /// Queries the specified SQL.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <typeparam name="T3">The type of the 3.</typeparam>
        /// <typeparam name="T4">The type of the 4.</typeparam>
        /// <param name="sql">The SQL.</param>
        /// <param name="map">The map.</param>
        /// <param name="splitOn">The split on.</param>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        IEnumerable<T> Query<T1, T2, T3, T4>(string sql, Func<T1, T2, T3, T4, T> map, string splitOn = "", object param = null);

        /// <summary>
        /// Queries the specified SQL.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <typeparam name="T3">The type of the 3.</typeparam>
        /// <typeparam name="T4">The type of the 4.</typeparam>
        /// <typeparam name="T5">The type of the 5.</typeparam>
        /// <param name="sql">The SQL.</param>
        /// <param name="map">The map.</param>
        /// <param name="splitOn">The split on.</param>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        IEnumerable<T> Query<T1, T2, T3, T4, T5>(string sql, Func<T1, T2, T3, T4, T5, T> map, string splitOn = "", object param = null);

        /// <summary>
        /// Queries the specified SQL.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <typeparam name="T3">The type of the 3.</typeparam>
        /// <typeparam name="T4">The type of the 4.</typeparam>
        /// <typeparam name="T5">The type of the 5.</typeparam>
        /// <typeparam name="T6">The type of the 6.</typeparam>
        /// <param name="sql">The SQL.</param>
        /// <param name="map">The map.</param>
        /// <param name="splitOn">The split on.</param>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        IEnumerable<T> Query<T1, T2, T3, T4, T5, T6>(string sql, Func<T1, T2, T3, T4, T5, T6, T> map, string splitOn = "", object param = null);

        /// <summary>
        /// Queries the specified SQL.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <typeparam name="T3">The type of the 3.</typeparam>
        /// <typeparam name="T4">The type of the 4.</typeparam>
        /// <typeparam name="T5">The type of the 5.</typeparam>
        /// <typeparam name="T6">The type of the 6.</typeparam>
        /// <typeparam name="T7">The type of the 7.</typeparam>
        /// <param name="sql">The SQL.</param>
        /// <param name="map">The map.</param>
        /// <param name="splitOn">The split on.</param>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        IEnumerable<T> Query<T1, T2, T3, T4, T5, T6, T7>(string sql, Func<T1, T2, T3, T4, T5, T6, T7, T> map, string splitOn = "", object param = null);

        /// <summary>
        /// Queries the dynamic.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="map">The map.</param>
        /// <param name="param">The param.</param>
        /// <returns></returns>
        IEnumerable<dynamic> QueryDynamic(string sql, Func<T, T, T> map, object param = null);

        /// <summary>
        /// Queries the dynamic.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="map">The map.</param>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        IEnumerable<dynamic> QueryDynamic(string sql, Func<T, T, T, T> map, object param = null);

        /// <summary>
        /// Queries the dynamic.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <param name="sql">The SQL.</param>
        /// <param name="map">The map.</param>
        /// <param name="splitOn">The split on.</param>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        IEnumerable<dynamic> QueryDynamic<T1, T2>(string sql, Func<T1, T2, T> map, string splitOn = "", object param = null);

        /// <summary>
        /// Queries the dynamic.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <typeparam name="T3">The type of the 3.</typeparam>
        /// <param name="sql">The SQL.</param>
        /// <param name="map">The map.</param>
        /// <param name="splitOn">The split on.</param>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        IEnumerable<dynamic> QueryDynamic<T1, T2, T3>(string sql, Func<T1, T2, T3, T> map, string splitOn = "", object param = null);

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetList();

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="whereConditions">The where conditions.</param>
        /// <returns></returns>
        IEnumerable<T> GetList(object whereConditions);

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="conditions">The conditions.</param>
        /// <returns></returns>
        IEnumerable<T> GetList(string conditions);

        /// <summary>
        /// Gets the list paged.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="rowsPerPage">The rows per page.</param>
        /// <param name="conditions">The conditions.</param>
        /// <param name="orderby">The orderby.</param>
        /// <returns></returns>
        IEnumerable<T> GetListPaged(int pageNumber, int rowsPerPage, string conditions, string orderby);

        /// <summary>
        /// Gets the list asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetListAsync();

        /// <summary>
        /// Gets the list asynchronous.
        /// </summary>
        /// <param name="whereConditions">The where conditions.</param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetListAsync(object whereConditions);

        /// <summary>
        /// Gets the list asynchronous.
        /// </summary>
        /// <param name="conditions">The conditions.</param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetListAsync(string conditions);

        /// <summary>
        /// Gets the list paged asynchronous.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="rowsPerPage">The rows per page.</param>
        /// <param name="conditions">The conditions.</param>
        /// <param name="orderby">The orderby.</param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetListPagedAsync(int pageNumber, int rowsPerPage, string conditions, string orderby);

        /// <summary>
        /// Records the count.
        /// </summary>
        /// <param name="conditions">The conditions.</param>
        /// <returns></returns>
        int RecordCount(string conditions = "");

        /// <summary>
        /// Records the count asynchronous.
        /// </summary>
        /// <param name="conditions">The conditions.</param>
        /// <returns></returns>
        Task<int> RecordCountAsync(string conditions);

        /// <summary>
        /// Queries the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        IEnumerable<T> Query(BaseQuery query);

        /// <summary>
        /// Queries the specified SQL.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="types">The types.</param>
        /// <param name="map">The map.</param>
        /// <param name="param">The parameter.</param>
        /// <param name="splitOn">The split on.</param>
        /// <returns></returns>
        IEnumerable<T> Query(string sql, Type[] types, Func<object[], T> map, object param = null, string splitOn = "");

        #endregion
    }
}
