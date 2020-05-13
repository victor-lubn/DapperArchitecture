using RS.Domain.Models;
using RS.Domain.Models.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace RS.Repositories.QueryBuilders.Contracts
{
    public interface IAppUserQueryBuilder
    {
        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        BaseQuery GetUsers(AppUserFilter filter);

        /// <summary>
        /// Gets the users include orders.
        /// </summary>
        /// <param name="splitOn">The split on.</param>
        /// <returns></returns>
        BaseQuery GetUsersIncludeOrders(out string splitOn);
    }
}
