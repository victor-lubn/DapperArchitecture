using RS.Services.Base.Contracts;
using RS.Services.Contracts;

namespace RS.Services.Base
{
    /// <summary>
    /// The BaseService
    /// </summary>
    /// <seealso cref="RS.Services.Base.Contracts.IBaseService" />
    public class BaseService : IBaseService
    {
        /// <summary>
        /// Gets or sets the service factory.
        /// </summary>
        /// <value>
        /// The service factory.
        /// </value>
        protected IServiceFactory ServiceFactory { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService"/> class.
        /// </summary>
        /// <param name="serviceFactory">The service factory.</param>
        public BaseService(IServiceFactory serviceFactory)
        {
            ServiceFactory = serviceFactory;
        }
    }
}
