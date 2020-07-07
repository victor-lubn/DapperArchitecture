using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using RS.Repositories.Factory.Contracts;

namespace RS.Api.Attributes
{
    /// <summary>
    /// The unit of work action filter.
    /// </summary>
    public class UnitOfWorkActionFilter : ActionFilterAttribute
    {
        /// <summary>
        /// Occurs after the action method is invoked.
        /// </summary>
        /// <param name="context">The action executed context.</param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            var connectionFactory = context.HttpContext.RequestServices.GetService<IConnectionFactory>();

            if (context.Exception == null)
            {
                connectionFactory.Commit();
            }
            else
            {
                connectionFactory.Rollback();
            }
        }
    }
}
