using RS.Domain.Models.Data;
using RS.Domain.Models.Filters;
using RS.Domain.Models.Views;
using RS.Services.Base.Contracts;
using System.Collections.Generic;

namespace RS.Services.Data.Contracts
{
    /// <summary>
    /// The IAppUserService
    /// </summary>
    /// <seealso cref="RS.Services.Base.Contracts.IBaseDataService{RS.Domain.Models.Data.User}" />
    public interface IAppUserService : IBaseDataService<AppUser>
    {
        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        AppUserModel GetUser(AppUserFilter filter);

        /// <summary>
        /// Gets the user include orders.
        /// </summary>
        /// <returns></returns>
        IEnumerable<AppUser> GetUsersIncludeOrders();
    }
}