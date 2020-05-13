using System.Data;
using RS.Repositories.Factory.Contracts;

namespace RS.Repositories.Factory
{
    /// <summary>
    /// Class ConnectionFactory
    /// </summary>
    public class ConnectionFactory : BaseConnectionFactory, IConnectionFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionFactory" /> class.
        /// </summary>
        /// <param name="enableTransaction">If set to <c>true</c> then enable transaction.</param>
        /// <param name="dbConnection">If set to <c>true</c> then enable transaction.</param>

        public ConnectionFactory(IDbConnection dbConnection, bool enableTransaction = false) : base(dbConnection)
        {
            EnableDbTransaction = enableTransaction;
        }
    }
}
