using RS.Repositories.QueryBuilders.Contracts;

namespace RS.Repositories.QueryBuilders.Factory.Contracts
{
    /// <summary>
    /// The query builder factory.
    /// </summary>
    public interface IQueryBuilderFactory
    {
        /// <summary>
        /// Gets the common case note query builder.
        /// </summary>
        /// <value>
        /// The common case note query builder.
        /// </value>
        IAppUserQueryBuilder AppUserQueryBuilder { get; }
    }
}