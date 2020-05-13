using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using RS.Common.Extensions;
using RS.Common.Helpers;
using RS.Domain.Models;
using RS.Repositories.Contracts;
using RS.Repositories.Factory.Contracts;

namespace RS.Repositories
{
    /// <summary>
    /// The dapper repository.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DbRepository<T> : DapperRepository, IDbRepository<T> where T : BaseDbModel, new()
    {
        #region Properties

        /// <summary>
        /// Initializes a new instance of the <see cref="DbRepository{T}" /> class.
        /// </summary>
        /// <param name="connectionFactory">The connection factory.</param>
        /// <param name="memoryCacheService">The memory cache service.</param>
        public DbRepository(IConnectionFactory connectionFactory)
            : base(connectionFactory)
        {
        }

        #endregion

        #region IDbRepository

        #region Get

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public T Get(object id)
        {
            return ConnectionFactory.DbConnection.Get<T>(id, ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Task<T> GetAsync(object id)
        {
            return ConnectionFactory.DbConnection.GetAsync<T>(id, ConnectionFactory.DbTransaction);
        }

        #endregion

        #region Delete

        /// <summary>
        /// Deletes the specified entity to delete.
        /// </summary>
        /// <param name="entityToDelete">The entity to delete.</param>
        /// <returns></returns>
        public int Delete(T entityToDelete)
        {
            Check.Argument.IsNotNull(entityToDelete, "entityToDelete");
            return ConnectionFactory.DbConnection.Delete(entityToDelete, ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public int Delete(object id)
        {
            if (id is int)
                Check.Argument.IsNotNegativeOrZero(id.ToInt(0), "id");

            if (id is Guid)
                Check.Argument.IsNotEmpty(id.ToGuid(), "id");

            return ConnectionFactory.DbConnection.Delete<T>(id, ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Deletes the list.
        /// </summary>
        /// <param name="entitiesIds">The entities identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Key attribute not found</exception>
        public int DeleteList(IList<int> entitiesIds)
        {
            Check.Argument.IsNotEmpty(entitiesIds.ToList(), "entitiesIds");
            var key = typeof(T).GetProperties().FirstOrDefault(p => p.GetCustomAttributes(typeof(KeyAttribute), true).Length != 0);
            if (key == null)
                throw new ArgumentException("Key attribute not found");

            return DeleteList($" where {key.Name} in ({String.Join(",", entitiesIds)})");
        }

        /// <summary>
        /// Deletes the list.
        /// </summary>
        /// <param name="whereConditions">The where conditions.</param>
        /// <returns></returns>;
        public int DeleteList(object whereConditions)
        {
            Check.Argument.IsNotNull(whereConditions, "whereConditions");
            return ConnectionFactory.DbConnection.DeleteList<T>(whereConditions, ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Deletes the list.
        /// </summary>
        /// <param name="conditions">The conditions.</param>
        /// <returns></returns>
        public int DeleteList(string conditions)
        {
            Check.Argument.IsNotEmpty(conditions, "conditions");
            return ConnectionFactory.DbConnection.DeleteList<T>(conditions, parameters: null, transaction: ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Deletes the list asynchronous.
        /// </summary>
        /// <param name="entitiesIds">The entities ids.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Key attribute not found</exception>
        public Task<int> DeleteListAsync(IList<int> entitiesIds)
        {
            Check.Argument.IsNotEmpty(entitiesIds.ToList(), "entitiesIds");
            var key = typeof(T).GetProperties().FirstOrDefault(p => p.GetCustomAttributes(typeof(KeyAttribute), true).Length != 0);
            if (key == null)
                throw new ArgumentException("Key attribute not found");

            return DeleteListAsync($" where {key.Name} in ({String.Join(",", entitiesIds)})");
        }

        /// <summary>
        /// Deletes the list.
        /// </summary>
        /// <param name="conditions">The conditions.</param>
        /// <returns></returns>
        public Task<int> DeleteListAsync(string conditions)
        {
            Check.Argument.IsNotEmpty(conditions, "conditions");
            return ConnectionFactory.DbConnection.DeleteListAsync<T>(conditions, parameters: null, transaction: ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Deletes the list asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereConditions">The where conditions.</param>
        /// <returns></returns>
        public Task<int> DeleteListAsync(object whereConditions)
        {
            Check.Argument.IsNotNull(whereConditions, "whereConditions");
            return ConnectionFactory.DbConnection.DeleteListAsync<T>(whereConditions, ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="entityToDelete">The entity to delete.</param>
        /// <returns></returns>
        public Task<int> DeleteAsync(T entityToDelete)
        {
            Check.Argument.IsNotNull(entityToDelete, "entityToDelete");
            return ConnectionFactory.DbConnection.DeleteAsync(entityToDelete, ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Task<int> DeleteAsync(object id)
        {
            Check.Argument.IsNotNegativeOrZero(id.ToInt(0), "id");
            return ConnectionFactory.DbConnection.DeleteAsync<T>(id, ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="entitiesToDelete">The entities to delete.</param>
        /// <returns></returns>
        public async Task DeleteListAsync(List<T> entitiesToDelete)
        {
            Check.Argument.IsNotEmpty(entitiesToDelete, "entitiesToDelete");
            foreach (var entityToDelete in entitiesToDelete)
            {
                await DeleteAsync(entityToDelete);
            }
        }

        #endregion

        #region Query

        /// <summary>
        /// Queries the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        public IEnumerable<T> Query(string query, object param = null)
        {
            return ConnectionFactory.DbConnection.Query<T>(query, param, ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Queries the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public IEnumerable<T> Query(BaseQuery query)
        {
            return ConnectionFactory.DbConnection.Query<T>(query.Query, query.Param, ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Queries the specified SQL.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="map">The map.</param>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        public IEnumerable<T> Query(string sql, Func<T, T, T> map, object param = null)
        {
            return ConnectionFactory.DbConnection.Query(sql, map, param, ConnectionFactory.DbTransaction).AsQueryable();
        }

        /// <summary>
        /// Queries the specified SQL.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="types">The types.</param>
        /// <param name="map">The map.</param>
        /// <param name="param">The parameter.</param>
        /// <param name="splitOn">The split on.</param>
        /// <returns></returns>
        public IEnumerable<T> Query(string sql, Type[] types, Func<object[], T> map, object param = null, string splitOn = "")
        {
            return ConnectionFactory.DbConnection.Query<T>(sql, types, map, param, ConnectionFactory.DbTransaction, splitOn: splitOn);
        }

        /// <summary>
        /// Queries the specified SQL.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="map">The map.</param>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        public IEnumerable<T> Query(string sql, Func<T, T, T, T> map, object param = null)
        {
            return ConnectionFactory.DbConnection.Query(sql, map, param, ConnectionFactory.DbTransaction).AsQueryable();
        }

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
        public IEnumerable<T> Query<T1, T2>(string sql, Func<T1, T2, T> map, string splitOn = "", object param = null)
        {
            return ConnectionFactory.DbConnection.Query(sql, map, param, ConnectionFactory.DbTransaction, splitOn: splitOn).AsQueryable();
        }

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
        public IEnumerable<T> Query<T1, T2, T3>(string sql, Func<T1, T2, T3, T> map, string splitOn = "", object param = null)
        {
            return ConnectionFactory.DbConnection.Query(sql, map, param, ConnectionFactory.DbTransaction, splitOn: splitOn).AsQueryable();
        }

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
        public IEnumerable<T> Query<T1, T2, T3, T4>(string sql, Func<T1, T2, T3, T4, T> map, string splitOn = "", object param = null)
        {
            return ConnectionFactory.DbConnection.Query(sql, map, param, ConnectionFactory.DbTransaction, splitOn: splitOn).AsQueryable();
        }

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
        public IEnumerable<T> Query<T1, T2, T3, T4, T5>(string sql, Func<T1, T2, T3, T4, T5, T> map, string splitOn = "", object param = null)
        {
            return ConnectionFactory.DbConnection.Query(sql, map, param, ConnectionFactory.DbTransaction, splitOn: splitOn).AsQueryable();
        }

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
        public IEnumerable<T> Query<T1, T2, T3, T4, T5, T6>(string sql, Func<T1, T2, T3, T4, T5, T6, T> map, string splitOn = "", object param = null)
        {
            return ConnectionFactory.DbConnection.Query(sql, map, param, ConnectionFactory.DbTransaction, splitOn: splitOn).AsQueryable();
        }

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
        public IEnumerable<T> Query<T1, T2, T3, T4, T5, T6, T7>(string sql, Func<T1, T2, T3, T4, T5, T6, T7, T> map, string splitOn = "", object param = null)
        {
            return ConnectionFactory.DbConnection.Query(sql, map, param, ConnectionFactory.DbTransaction, splitOn: splitOn).AsQueryable();
        }

        /// <summary>
        /// Queries the dynamic.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="map">The map.</param>
        /// <param name="param">The param.</param>
        /// <returns></returns>
        public IEnumerable<dynamic> QueryDynamic(string sql, Func<T, T, T> map, object param = null)
        {
            return ConnectionFactory.DbConnection.Query(sql, map, param, ConnectionFactory.DbTransaction).AsQueryable();
        }

        /// <summary>
        /// Queries the dynamic.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="map">The map.</param>
        /// <param name="param">The param.</param>
        /// <returns></returns>
        public IEnumerable<dynamic> QueryDynamic(string sql, Func<T, T, T, T> map, object param = null)
        {
            return ConnectionFactory.DbConnection.Query(sql, map, param, ConnectionFactory.DbTransaction).AsQueryable();
        }

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
        public IEnumerable<dynamic> QueryDynamic<T1, T2>(string sql, Func<T1, T2, T> map, string splitOn = "", object param = null)
        {
            return ConnectionFactory.DbConnection.Query(sql, map, param, ConnectionFactory.DbTransaction, splitOn: splitOn).AsQueryable();
        }

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
        public IEnumerable<dynamic> QueryDynamic<T1, T2, T3>(string sql, Func<T1, T2, T3, T> map, string splitOn = "", object param = null)
        {
            return ConnectionFactory.DbConnection.Query(sql, map, param, ConnectionFactory.DbTransaction, splitOn: splitOn).AsQueryable();
        }

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetList()
        {
            return ConnectionFactory.DbConnection.GetList<T>(null, parameters: null, transaction: ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="whereConditions">The where conditions.</param>
        /// <returns></returns>
        public IEnumerable<T> GetList(object whereConditions)
        {
            return ConnectionFactory.DbConnection.GetList<T>(whereConditions, ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="conditions">The conditions.</param>
        /// <returns></returns>
        public IEnumerable<T> GetList(string conditions)
        {
            return ConnectionFactory.DbConnection.GetList<T>(conditions, parameters: null, transaction: ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Gets the list paged.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="rowsPerPage">The rows per page.</param>
        /// <param name="conditions">The conditions.</param>
        /// <param name="orderby">The orderby.</param>
        /// <returns></returns>
        public IEnumerable<T> GetListPaged(int pageNumber, int rowsPerPage, string conditions, string orderby)
        {
            return ConnectionFactory.DbConnection.GetListPaged<T>(pageNumber, rowsPerPage, conditions, orderby, ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Gets the list asynchronous.
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<T>> GetListAsync()
        {
            return ConnectionFactory.DbConnection.GetListAsync<T>(null, parameters: null, transaction: ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Gets the list asynchronous.
        /// </summary>
        /// <param name="whereConditions">The where conditions.</param>
        /// <returns></returns>
        public Task<IEnumerable<T>> GetListAsync(object whereConditions)
        {
            return ConnectionFactory.DbConnection.GetListAsync<T>(whereConditions, ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Gets the list asynchronous.
        /// </summary>
        /// <param name="conditions">The conditions.</param>
        /// <returns></returns>
        public Task<IEnumerable<T>> GetListAsync(string conditions)
        {
            return ConnectionFactory.DbConnection.GetListAsync<T>(conditions, parameters: null, transaction: ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Gets the list paged asynchronous.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="rowsPerPage">The rows per page.</param>
        /// <param name="conditions">The conditions.</param>
        /// <param name="orderby">The orderby.</param>
        /// <returns></returns>
        public Task<IEnumerable<T>> GetListPagedAsync(int pageNumber, int rowsPerPage, string conditions, string orderby)
        {
            return ConnectionFactory.DbConnection.GetListPagedAsync<T>(pageNumber, rowsPerPage, conditions, orderby);
        }

        /// <summary>
        /// Records the count.
        /// </summary>
        /// <param name="conditions">The conditions.</param>
        /// <returns></returns>
        public int RecordCount(string conditions = "")
        {
            return ConnectionFactory.DbConnection.RecordCount<T>(conditions, parameters: null, transaction: ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Records the count asynchronous.
        /// </summary>
        /// <param name="conditions">The conditions.</param>
        /// <returns></returns>
        public async Task<int> RecordCountAsync(string conditions)
        {
            return await ConnectionFactory.DbConnection.RecordCountAsync<T>(conditions, parameters: null, transaction: ConnectionFactory.DbTransaction);
        }

        #endregion

        #endregion
    }
}
