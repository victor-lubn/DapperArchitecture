using System;
using RS.Domain.Models.Data;
using RS.Repositories.QueryBuilders.Factory.Contracts;
using RS.Repositories.Factory.Contracts;
using RS.Services.Base;
using RS.Services.Contracts;
using RS.Services.Data.Contracts;
using RS.Domain.Models.Views;
using RS.Domain.Models.Filters;
using System.Linq;
using System.Collections.Generic;

namespace RS.Services.Data
{
    /// <summary>
    /// The AppUserService
    /// </summary>
    /// <seealso cref="RS.Services.Base.BaseDataService{RS.Domain.Models.Data.User}" />
    /// <seealso cref="IAppUserService" />
    public class AppUserService : BaseDataService<AppUser>, IAppUserService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppUserService"/> class.
        /// </summary>
        /// <param name="serviceFactory">The service factory.</param>
        /// <param name="queryBuilderFactory">The query builder factory.</param>
        /// <param name="repositoryFactory">The repository factory.</param>
        //// TODO remove repositoryFactory
        public AppUserService(
            IServiceFactory serviceFactory,
            IQueryBuilderFactory queryBuilderFactory,
            IRepositoryFactory repositoryFactory)
            : base(serviceFactory, queryBuilderFactory, repositoryFactory) 
        {
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public AppUserModel GetUser(AppUserFilter filter)
        {
            var baseQuery = QueryBuilderFactory.AppUserQueryBuilder.GetUsers(filter);
            var items = Repository.Query<AppUserModel>(baseQuery.Query, baseQuery.Param);
            return items.FirstOrDefault();
        }

        /// <summary>
        /// Gets the users include orders.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AppUser> GetUsersIncludeOrders()
        {
            // TODO change to Return AppUserModel
            var splitOn = String.Empty;
            var query = QueryBuilderFactory.AppUserQueryBuilder.GetUsersIncludeOrders(out splitOn);
            var preResult = new Dictionary<Guid, AppUser>();

            var func = new Func<AppUser, AppOrder, AppUser>((u, o) =>
            {
                if (!preResult.TryGetValue(u.AppUserId, out var appUser))
                {
                    preResult.Add(u.AppUserId, appUser = u);
                }

                return appUser;
            });

            Repository.Query(query.Query, func, splitOn);
            return preResult.Values;
        }
    }
}
