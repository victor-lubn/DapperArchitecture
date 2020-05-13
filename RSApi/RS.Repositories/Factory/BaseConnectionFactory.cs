using System;
using System.Data;
using RS.Repositories.Factory.Contracts;

namespace RS.Repositories.Factory
{
    /// <summary>
    /// The base connection factory.
    /// </summary>
    public abstract class BaseConnectionFactory : IBaseConnectionFactory, IDisposable
    {
        #region Properties

        /// <summary>
        /// Gets the database connection.
        /// </summary>
        /// <value>
        /// The database connection.
        /// </value>
        public IDbConnection DbConnection { get; set; }
        
        /// <summary>
        /// The database transaction.
        /// </summary>
        private IDbTransaction dbTransaction;

        /// <summary>
        /// Gets or sets a value indicating whether enable database transaction.
        /// </summary>
        /// <value>
        /// A value indicating whether enable database transaction.
        /// </value>
        public bool EnableDbTransaction { get; set; }

        /// <summary>
        /// Gets the database transaction.
        /// </summary>
        /// <value>
        /// The database transaction.
        /// </value>
        public IDbTransaction DbTransaction => EnableDbTransaction ? (dbTransaction ?? (dbTransaction = DbConnection.BeginTransaction())) : null;

        #endregion

        #region BaseConnectionFactory

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseConnectionFactory"/> class.
        /// </summary>
        /// <param name="dbConnection">The container.</param>
        protected BaseConnectionFactory(IDbConnection dbConnection)
        {
            DbConnection = dbConnection;
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="BaseConnectionFactory"/> is reclaimed by garbage collection.
        /// </summary>
        ~BaseConnectionFactory()
        {
            Dispose(false);
        }

        #endregion

        #region IBaseConnectionFactory

        /// <summary>
        /// Commits this instance.
        /// </summary>
        public void Commit()
        {
            try
            {
                if (DbConnection == null || dbTransaction == null)
                    return;

                if (DbConnection.State == ConnectionState.Closed)
                    return;

                if (dbTransaction.Connection != null)
                    dbTransaction.Commit();

                if (DbConnection.State != ConnectionState.Closed)
                    DbConnection.Close();
            }
            catch (Exception e)
            {
                if (e is ObjectDisposedException || e is InvalidOperationException)
                {
                    Console.WriteLine(e.Message);
                    return;
                }

                throw;
            }
        }

        /// <summary>
        /// Rollbacks this instance.
        /// </summary>
        public void Rollback()
        {
            try
            {
                if (DbConnection == null || dbTransaction == null)
                    return;

                if (dbTransaction.Connection != null)
                    dbTransaction.Rollback();
                if (DbConnection.State != ConnectionState.Closed)
                    DbConnection.Close();
            }
            catch (ObjectDisposedException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        #endregion

        #region IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Helpers

        /// <summary>
        /// The disposed.
        /// </summary>
        private bool disposed;

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                Commit();
                if (dbTransaction != null)
                    dbTransaction.Dispose();

                if (DbConnection != null)
                    DbConnection.Dispose();
            }

            DbConnection = null;
            dbTransaction = null;

            disposed = true;
        }

        #endregion
    }
}
