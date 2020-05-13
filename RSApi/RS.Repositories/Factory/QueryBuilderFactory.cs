using RS.Repositories.QueryBuilders.Contracts;
using RS.Repositories.QueryBuilders.Factory.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace RS.Repositories.QueryBuilders.Factory
{
    /// <summary>
    /// The data service factory.
    /// </summary>
    public class QueryBuilderFactory : IQueryBuilderFactory
    {
        #region Properties

        private readonly IServiceProvider provider;

        #endregion

        #region QueryBuilderFactory
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryBuilderFactory" /> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public QueryBuilderFactory(IServiceProvider provider)
        {
            this.provider = provider;
        }
        #endregion

        #region QueryBuilders

        public IAppUserQueryBuilder AppUserQueryBuilder => provider.GetService<IAppUserQueryBuilder>();

        #endregion
    }
}