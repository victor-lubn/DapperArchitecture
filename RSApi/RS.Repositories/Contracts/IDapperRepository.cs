using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RS.Domain.Models;

namespace RS.Repositories.Contracts
{  
        /// <summary>
        /// The i common repository.
        /// </summary>
        public interface IDapperRepository
        {
            #region Save

            /// <summary>
            /// Saves the specified entity.
            /// </summary>
            /// <param name="entity">The entity.</param>
            /// <returns></returns>
            int Save(BaseModel entity);

            /// <summary>
            /// Saves the asynchronous.
            /// </summary>
            /// <param name="entity">The entity.</param>
            /// <returns></returns>
            Task<int?> SaveAsync(BaseModel entity);

            /// <summary>
            /// Saves the unique identifier.
            /// </summary>
            /// <param name="entity">The entity.</param>
            /// <returns></returns>
            Guid SaveGuid(BaseModel entity);

            /// <summary>
            /// Saves the unique identifier asynchronous.
            /// </summary>
            /// <param name="entity">The entity.</param>
            /// <returns></returns>
            Task<Guid> SaveGuidAsync(BaseModel entity);

            #endregion

            #region Insert

            /// <summary>
            /// Inserts the specified entity to insert.
            /// </summary>
            /// <param name="entityToInsert">The entity to insert.</param>
            /// <returns></returns>
            int? Insert(object entityToInsert);

            /// <summary>
            /// Inserts the specified entity to insert.
            /// </summary>
            /// <typeparam name="TKey">The type of the key.</typeparam>
            /// <param name="entityToInsert">The entity to insert.</param>
            /// <returns></returns>
            TKey Insert<TKey>(object entityToInsert);

            /// <summary>
            /// Inserts the specified entity to insert.
            /// </summary>
            /// <typeparam name="TKey">The type of the key.</typeparam>
            /// <typeparam name="TEntity">The type of the entity.</typeparam>
            /// <param name="entityToInsert">The entity to insert.</param>
            /// <returns></returns>
            TKey Insert<TKey, TEntity>(TEntity entityToInsert);

            /// <summary>
            /// Inserts the asynchronous.
            /// </summary>
            /// <param name="entityToInsert">The entity to insert.</param>
            /// <returns></returns>
            Task<int?> InsertAsync(object entityToInsert);

            /// <summary>
            /// Inserts the asynchronous.
            /// </summary>
            /// <typeparam name="TKey">The type of the key.</typeparam>
            /// <param name="entityToInsert">The entity to insert.</param>
            /// <returns></returns>
            Task<TKey> InsertAsync<TKey>(object entityToInsert);

            #endregion

            #region Update

            /// <summary>
            /// Updates the specified entity to update.
            /// </summary>
            /// <param name="entityToUpdate">The entity to update.</param>
            void Update(object entityToUpdate);

            /// <summary>
            /// Updates the asynchronous.
            /// </summary>
            /// <param name="entityToUpdate">The entity to update.</param>
            /// <returns></returns>
            //// TODO : Buy license [TimeStamp(AttributeInheritance=MulticastInheritance.Multicast)] 
            Task<int?> UpdateAsync(object entityToUpdate);

            #endregion

            #region Query

            /// <summary>
            /// Queries the specified query.
            /// </summary>
            /// <typeparam name="K"></typeparam>
            /// <param name="query">The query.</param>
            /// <returns></returns>
            IEnumerable<K> Query<K>(BaseQuery query);

            /// <summary>
            /// Queries the specified query.
            /// </summary>
            /// <typeparam name="K"></typeparam>
            /// <param name="query">The query.</param>
            /// <param name="param">The parameter.</param>
            /// <returns></returns>
            IEnumerable<K> Query<K>(string query, object param);

            /// <summary>
            /// Queries the specified query.
            /// </summary>
            /// <typeparam name="K"></typeparam>
            /// <param name="query">The query.</param>
            /// <param name="param">The parameter.</param>
            /// <param name="commandTimeout">The command timeout.</param>
            /// <returns></returns>
            IEnumerable<K> Query<K>(string query, object param, int? commandTimeout);

            /// <summary>
            /// Queries the dynamic.
            /// </summary>
            /// <param name="query">The query.</param>
            /// <param name="param">The param.</param>
            /// <returns></returns>
            IEnumerable<dynamic> QueryDynamic(string query, object param);

            /// <summary>
            /// Queries the dynamic.
            /// </summary>
            /// <param name="query">The query.</param>
            /// <returns></returns>
            IEnumerable<dynamic> QueryDynamic(BaseQuery query);

            /// <summary>
            /// Executes the scalar.
            /// </summary>
            /// <typeparam name="K"></typeparam>
            /// <param name="sql">The SQL.</param>
            /// <param name="param">The parameter.</param>
            /// <returns></returns>
            K ExecuteScalar<K>(string sql, object param = null);

            /// <summary>
            /// Executes the scalar.
            /// </summary>
            /// <typeparam name="K"></typeparam>
            /// <param name="query">The query.</param>
            /// <returns></returns>
            K ExecuteScalar<K>(BaseQuery query);

            /// <summary>
            /// Executes the sp.
            /// </summary>
            /// <param name="spName">Name of the sp.</param>
            /// <param name="whereConditions">The where conditions.</param>
            /// <returns></returns>
            dynamic ExecuteSp(string spName, object whereConditions = null);

            #endregion
        }
    }
