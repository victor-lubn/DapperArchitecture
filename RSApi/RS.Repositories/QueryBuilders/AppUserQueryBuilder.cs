using RS.Domain.Models;
using RS.Domain.Models.Filters;
using RS.Repositories.QueryBuilders.Base;
using RS.Repositories.QueryBuilders.Contracts;
using RS.Repositories.QueryBuilders.Factory.Contracts;
using RS.Repositories.QueryFilterBuilder;
using System;
using System.Collections.Generic;
using System.Text;

namespace RS.Repositories.QueryBuilders
{
    public class AppUserQueryBuilder : BaseQueryBuilder, IAppUserQueryBuilder
    {
        #region AppUserQueryBuilder
        /// <summary>
        /// Initializes a new instance of the <see cref="CommonUserQueryBuilder" /> class.
        /// </summary>
        /// <param name="queryBuilderFactory">The query builder factory.</param>
        public AppUserQueryBuilder(IQueryBuilderFactory queryBuilderFactory) : base(queryBuilderFactory)
        {
        }
        #endregion

        public BaseQuery GetUsers(AppUserFilter filter)
        {
            return
                GetUsersJoined(
                    filter,
                    @"this_.AppUserId AS UserId,
                        (this_.LastName + ', ' + this_.FirstName) AS FullName,
                        this_.UserName,
                        this_.Email,
                        this_.IsActive,
                        userRole_.Name as RoleName");
        }

        public BaseQuery GetUsersIncludeOrders(out string splitOn)
        {
            splitOn = "AppUserRoleId,AppOrderId".Trim();
            return
                GetUsersIncludeOrdersJoined(
                    @"this_.*,
                    userRole_.*,
                    order_.*");
        }

        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="selectList">The select list.</param>
        /// <returns></returns>
        private BaseQuery GetUsersJoined(AppUserFilter filter, string selectList)
        {
            var builder = new StringBuilder();

            builder.Append(
                @"SELECT
                    " + selectList + @"
                FROM [AppUser] this_
                INNER JOIN [AppUserRole] userRole_ on this_.AppUserRoleId = userRole_.AppUserRoleId
                WHERE (1 = 1)");

            return new BaseQuery(builder, null);
        }

        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <param name="selectList">The select list.</param>
        /// <returns></returns>
        private BaseQuery GetUsersIncludeOrdersJoined(string selectList)
        {
            var builder = new StringBuilder();

            builder.Append(
                @"SELECT
                    " + selectList + @"
                FROM [AppUser] this_
                INNER JOIN [AppUserRole] userRole_ on this_.AppUserRoleId = userRole_.AppUserRoleId
                INNER JOIN [AppOrder] order_ on this_.AppUserId = order_.AppUserId
                WHERE (1 = 1)");

            return new BaseQuery(builder, null);
        }
    }
}
