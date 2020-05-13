using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RS.Domain.Models;
using RS.Repositories.Contracts;
using RS.Repositories.Factory.Contracts;
using RS.Repositories.QueryBuilders.Factory.Contracts;
using RS.Services.Base.Contracts;
using RS.Services.Contracts;

namespace RS.Services.Base
{
    /// <summary>
    /// BaseDataService
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="RS.Services.Base.BaseService" />
    /// <seealso cref="RS.Services.Base.Contracts.IBaseDataService{T}" />
    public class BaseDataService<T> : BaseService, IBaseDataService<T> where T : BaseDbModel
    {
        /// <summary>
        /// Gets or sets the query builder factory.
        /// </summary>
        /// <value>
        /// The query builder factory.
        /// </value>
        protected IQueryBuilderFactory QueryBuilderFactory { get; private set; }
        /// <summary>
        /// Gets the repository.
        /// </summary>
        /// <value>
        /// The repository.
        /// </value>
        protected IDbRepository<T> Repository { get; private set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseDataService{T}"/> class.
        /// </summary>
        /// <param name="serviceFactory">The service factory.</param>
        /// <param name="queryBuilderFactory">The query builder factory.</param>
        /// <param name="repositoryFactory">The repository factory.</param>
        public BaseDataService(IServiceFactory serviceFactory, IQueryBuilderFactory queryBuilderFactory, IRepositoryFactory repositoryFactory) : base(serviceFactory)
        {
            QueryBuilderFactory = queryBuilderFactory;
            Repository = repositoryFactory.GetRepository<T>();
        }
        /// <summary>
        /// Runs the asynchronous.
        /// </summary>
        /// <param name="task">The task.</param>
        public void RunAsync(Task task)
        {
            task.ContinueWith(t => {}, TaskContinuationOptions.OnlyOnFaulted);
        }
        /// <summary>
        /// Creates the specified entity to insert.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="entityToInsert">The entity to insert.</param>
        /// <returns></returns>
        public TKey Create<TKey>(object entityToInsert)
        {
            return Repository.Insert<TKey>(entityToInsert);
        }
        /// <summary>
        /// Creates the specified entity to insert.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entityToInsert">The entity to insert.</param>
        /// <returns></returns>
        public TKey Create<TKey, TEntity>(TEntity entityToInsert)
        {
            return Repository.Insert<TKey, TEntity>(entityToInsert);
        }
        /// <summary>
        /// Creates the specified entity to insert.
        /// </summary>
        /// <param name="entityToInsert">The entity to insert.</param>
        /// <returns></returns>
        public int? Create(T entityToInsert)
        {
            return Repository.Insert(entityToInsert);
        }
        /// <summary>
        /// Updates the specified entity to insert.
        /// </summary>
        /// <param name="entityToInsert">The entity to insert.</param>
        public void Update(T entityToInsert)
        {
            Repository.Update(entityToInsert);
        }
        /// <summary>
        /// Deletes the specified entity to insert.
        /// </summary>
        /// <param name="entityToInsert">The entity to insert.</param>
        public void Delete(T entityToInsert)
        {
            Repository.Delete(entityToInsert);
        }
        /// <summary>
        /// Gets the by.
        /// </summary>
        /// <param name="whereContitions">The where contitions.</param>
        /// <returns></returns>
        public T GetBy(object whereContitions)
        {
            return Repository.GetList(whereContitions).FirstOrDefault();
        }
        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="whereContitions">The where contitions.</param>
        /// <returns></returns>
        public IEnumerable<T> GetList(object whereContitions)
        {
            return Repository.GetList(whereContitions);
        }

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetList()
        {
            return Repository.GetList();
        }
    }
}
