using RS.Common.Constants;
using RS.Common.Helpers;
using RS.Domain.Models.Data;
using RS.Domain.Models.Filters;
using RS.Repositories.QueryBuilders.Factory.Contracts;
using RS.Repositories.QueryFilterBuilder.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace RS.Repositories.QueryFilterBuilder
{
    internal class AppUserQueryFilterBuilder : BaseQueryFilterBuilder<AppUserFilter>
    {
        #region CommonUserQueryFilterBuilder
        /// <summary>
        /// Initializes a new instance of the <see cref="CommonUserQueryFilterBuilder" /> class.
        /// </summary>
        /// <param name="queryBuilderFactory">The query builder factory.</param>
        /// <param name="dataServiceFactory">The data service factory.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="alias">The alias.</param>
        /// <param name="param">The parameter.</param>
        /// <param name="secondAlias">The second alias.</param>
        public AppUserQueryFilterBuilder(
            IQueryBuilderFactory queryBuilderFactory,
            AppUserFilter filter,
            string alias,
            object param = null,
            string secondAlias = "")
            : base(queryBuilderFactory, filter, alias, param, secondAlias)
        {
            BuildQueryFilters();
        }
        #endregion

        #region Methods

        /// <summary>
        /// Builds the query filters.
        /// </summary>
        private void BuildQueryFilters()
        {
            if (Filter == null)
                return;

            if (!String.IsNullOrWhiteSpace(Filter.UserName))
            {
                DynamicHelper.SetProperty(Param, Parameters.UserName, Filter.UserName.Replace("_", "[_]"));

                var field = ReflectionHelper.GetString((AppUser x) => x.UserName);
                Conditions.Add(String.Format(ConditionLikeTemplate, Alias, field, "@" + Parameters.UserName));
            }

            if (!String.IsNullOrEmpty(Filter.FirstName))
            {
                DynamicHelper.SetProperty(Param, Parameters.FirstName, Filter.FirstName);

                var field = ReflectionHelper.GetString((AppUser x) => x.FirstName);
                Conditions.Add(String.Format(ConditionLikeTemplate, Alias, field, "@" + Parameters.FirstName + " + '%'"));
            }
        }

        #endregion
    }
}
