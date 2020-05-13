using System.Security.Claims;
using RS.Domain.Models.Views;

namespace RS.Services.Common.Contracts
{
    /// <summary>
    /// The IAccountService
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Gets the identity.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        ClaimsIdentity GetIdentity(string email, string password);
        /// <summary>
        /// Generates the token.
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <returns></returns>
        AppUserModel GenerateToken(ClaimsIdentity identity);
        /// <summary>
        /// Creates the claims.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        ClaimsIdentity CreateClaims(string userName);
    }
}