using System.Data;

namespace RS.Repositories.Factory.Contracts
{
    public interface IBaseConnectionFactory
    {
        /// <summary>
        /// Gets the database connection.
        /// </summary>
        /// <value>
        /// The database connection.
        /// </value>
        IDbConnection DbConnection { get; }

        /// <summary>
        /// Gets the database transaction.
        /// </summary>
        /// <value>
        /// The database transaction.
        /// </value>
        IDbTransaction DbTransaction { get; }

        /// <summary>
        /// Commits this instance.
        /// </summary>
        void Commit();

        /// <summary>
        /// Rollbacks this instance.
        /// </summary>
        void Rollback();
    }
}