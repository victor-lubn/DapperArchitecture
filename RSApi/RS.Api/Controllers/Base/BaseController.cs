using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RS.Api.Attributes;
using RS.Services.Contracts;

namespace RS.Api.Controllers.Base
{
    /// <summary>
    /// The base controller.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [EnableCors("RSOrigin")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [UnitOfWorkActionFilter]
    public class BaseController : Controller
    {
        /// <summary>
        /// Gets the service factory.
        /// </summary>
        /// <value>
        /// The service factory.
        /// </value>
        protected IServiceFactory ServiceFactory { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController"/> class.
        /// </summary>
        /// <param name="serviceFactory">The service factory.</param>
        public BaseController(IServiceFactory serviceFactory)
        {
            ServiceFactory = serviceFactory;
        }
    }
}