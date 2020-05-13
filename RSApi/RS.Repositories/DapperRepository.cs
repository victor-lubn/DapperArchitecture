using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using RS.Common.Constants;
using RS.Common.Extensions;
using RS.Domain.Models;
using RS.Repositories.Contracts;
using RS.Repositories.Factory.Contracts;

namespace RS.Repositories
{
    /// <summary>
    /// The common repository.
    /// </summary>
    /// <seealso cref="IDapperRepository" />
    public class DapperRepository : IDapperRepository
    {
        /// <summary>
        /// Gets the connection factory.
        /// </summary>
        /// <value>
        /// The connection factory.
        /// </value>
        protected IConnectionFactory ConnectionFactory { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DapperRepository"/> class.
        /// </summary>
        /// <param name="connectionFactory">The connection factory.</param>
        public DapperRepository(IConnectionFactory connectionFactory)
        {
            ConnectionFactory = connectionFactory;
        }

        #region ICommonRepository

        #region Save

        /// <summary>
        /// Updates the specified entity to update.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns></returns>
        public int Save(BaseModel entity)
        {
            var key = entity.GetType().GetProperties().FirstOrDefault(p => p.GetCustomAttributes(typeof(KeyAttribute), true).Length != 0);
            var objectId = key.GetValue(entity, null);

            if (objectId.ToInt() == 0)
                objectId = Insert(entity).ToInt(0);
            else
                Update(entity);

            return objectId.ToInt(0);
        }

        /// <summary>
        /// Updates the specified entity to update.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns></returns>
        public Guid SaveGuid(BaseModel entity)
        {
            var key = entity.GetType().GetProperties().FirstOrDefault(p => p.GetCustomAttributes(typeof(KeyAttribute), true).Length != 0);
            var objectId = key.GetValue(entity, null);

            if (objectId.ToGuid() == Guid.Empty)
            {
                key.SetValue(entity, GuidExtensions.GenerateComb());
                objectId = Insert<Guid>(entity);
            }
            else
            {
                Update(entity);
            }

            return objectId.ToGuid().Value;
        }

        /// <summary>
        /// Updates the specified entity to update.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns></returns>
        public Task<int?> SaveAsync(BaseModel entity)
        {
            var key = entity.GetType().GetProperties().FirstOrDefault(p => p.GetCustomAttributes(typeof(KeyAttribute), true).Length != 0);
            var objectId = key.GetValue(entity, null);

            return objectId.ToInt() == 0 ? InsertAsync(entity) : UpdateAsync(entity);
        }

        /// <summary>
        /// Updates the specified entity to update.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns></returns>
        public async Task<Guid> SaveGuidAsync(BaseModel entity)
        {
            var key = entity.GetType().GetProperties().FirstOrDefault(p => p.GetCustomAttributes(typeof(KeyAttribute), true).Length != 0);
            var objectId = key.GetValue(entity, null);
            if (objectId.ToGuid() == Guid.Empty)
            {
                return await InsertAsync<Guid>(entity);
            }

            await UpdateAsync(entity);
            return await new Task<Guid>(null);
        }

        #endregion

        #region Insert

        /// <summary>
        /// Inserts the specified entity to insert.
        /// </summary>
        /// <param name="entityToInsert">The entity to insert.</param>
        /// <returns></returns>
        public int? Insert(object entityToInsert)
        {
            return ConnectionFactory.DbConnection.Insert(entityToInsert, ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Inserts the specified entity to insert.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="entityToInsert">The entity to insert.</param>
        /// <returns></returns>
        public TKey Insert<TKey>(object entityToInsert)
        {
            return ConnectionFactory.DbConnection.Insert<TKey, object>(entityToInsert, ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Inserts the specified entity to insert.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entityToInsert">The entity to insert.</param>
        /// <returns></returns>
        public TKey Insert<TKey, TEntity>(TEntity entityToInsert)
        {
            return ConnectionFactory.DbConnection.Insert<TKey, TEntity>(entityToInsert, ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="entityToInsert">The entity to insert.</param>
        /// <returns></returns>
        public async Task<int?> InsertAsync(object entityToInsert)
        {
            return await ConnectionFactory.DbConnection.InsertAsync<int?, Object>(entityToInsert, ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="entityToInsert">The entity to insert.</param>
        /// <returns></returns>
        public async Task<TKey> InsertAsync<TKey>(object entityToInsert)
        {
            return await ConnectionFactory.DbConnection.InsertAsync<TKey, object>(entityToInsert, ConnectionFactory.DbTransaction);
        }

        #endregion

        #region Update

        /// <summary>
        /// Updates the specified entity to update.
        /// </summary>
        /// <param name="entityToUpdate">The entity to update.</param>
        public void Update(object entityToUpdate)
        {
            ConnectionFactory.DbConnection.Update(entityToUpdate, ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="entityToUpdate">The entity to update.</param>
        /// <returns>The number of effected records</returns>
        public async Task<int?> UpdateAsync(object entityToUpdate)
        {
            return await ConnectionFactory.DbConnection.UpdateAsync(entityToUpdate, ConnectionFactory.DbTransaction);
        }

        #endregion

        #region Query

        /// <summary>
        /// Queries the specified query.
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IEnumerable<K> Query<K>(BaseQuery query)
        {
            return ConnectionFactory.DbConnection.Query<K>(query.Query, AdjustTakeParameter(query.Param), ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Queries the specified query.
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        public IEnumerable<K> Query<K>(string query, object param = null)
        {
            return ConnectionFactory.DbConnection.Query<K>(query, AdjustTakeParameter(param), ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Queries the specified query.
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="param">The parameter.</param>
        /// <param name="commandTimeout">The command timeout.</param>
        /// <returns></returns>
        public IEnumerable<K> Query<K>(string query, object param = null, int? commandTimeout = null)
        {
            return ConnectionFactory.DbConnection.Query<K>(query, param, ConnectionFactory.DbTransaction, commandTimeout: commandTimeout);
        }

        /// <summary>
        /// Queries the dynamic.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="param">The param.</param>
        /// <returns></returns>
        public IEnumerable<dynamic> QueryDynamic(string query, object param)
        {
            return ConnectionFactory.DbConnection.Query(query, param, ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Queries the dynamic.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public IEnumerable<dynamic> QueryDynamic(BaseQuery query)
        {
            return ConnectionFactory.DbConnection.Query(query.Query, query.Param, ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Executes the scalar.
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <param name="sql">The SQL.</param>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        public K ExecuteScalar<K>(string sql, object param = null)
        {
            return ConnectionFactory.DbConnection.ExecuteScalar<K>(sql, param, ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Executes the scalar.
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public K ExecuteScalar<K>(BaseQuery query)
        {
            return ConnectionFactory.DbConnection.ExecuteScalar<K>(query.Query, query.Param, ConnectionFactory.DbTransaction);
        }

        /// <summary>
        /// Executes the sp.
        /// </summary>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="whereConditions">The where conditions.</param>
        /// <returns></returns>
        public dynamic ExecuteSp(string spName, object whereConditions = null)
        {
            return ConnectionFactory.DbConnection.ExecuteScalar(spName, whereConditions, ConnectionFactory.DbTransaction, commandType: CommandType.StoredProcedure);
        }
        #endregion

        #endregion

        #region Helpers

        /// <summary>
        /// Adjusts the take parameter.
        /// </summary>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        protected static object AdjustTakeParameter(object param)
        {
            if (param == null)
                return null;

            object takeValue = 0;
            if (((IDictionary<string, object>)param).TryGetValue(DbConstants.Take, out takeValue) &&
                Int32.Parse(takeValue.ToString()) == 0)
            {
                ((IDictionary<string, object>)param)[DbConstants.Take] = 1;
            }
            return param;
        }

        #endregion
    }
}
