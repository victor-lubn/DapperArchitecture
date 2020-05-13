using System.Text;

namespace RS.Domain.Models
{
    /// <summary>
    /// The base query.
    /// </summary>
    public class BaseQuery
    {
        #region Properties

        /// <summary>
        /// Gets the query.
        /// </summary>
        /// <value>
        /// The query.
        /// </value>
        public string Query
        {
            get { return Builder.ToString(); }
        }

        /// <summary>
        /// Gets the query.
        /// </summary>
        /// <value>
        /// The query.
        /// </value>
        public StringBuilder Builder { get; private set; }

        /// <summary>
        /// Gets the parameter.
        /// </summary>
        /// <value>
        /// The parameter.
        /// </value>
        public object Param { get; private set; }

        #endregion

        #region BaseQuery
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseQuery" /> class.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="param">The parameter.</param>
        public BaseQuery(StringBuilder builder, object param)
        {
            Builder = builder;
            Param = param;
        }
        #endregion
    }
}
