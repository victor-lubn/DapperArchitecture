using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using RS.Domain.Models.Options;
using RS.Domain.Models.Views;
using RS.Services.Base;
using RS.Services.Common.Contracts;
using RS.Services.Contracts;

namespace RS.Services.Common
{
    /// <summary>
    /// The AccountService
    /// </summary>
    /// <seealso cref="RS.Services.Base.BaseService" />
    /// <seealso cref="RS.Services.Common.Contracts.IAccountService" />
    public class AccountService : BaseService, IAccountService
    {
        public AccountService(IServiceFactory serviceFactory) : base(serviceFactory)
        {
        }
        /// <summary>
        /// Gets the identity.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public ClaimsIdentity GetIdentity(string email, string password)
        {
            var user = ServiceFactory.AppUserService.GetBy(new { email });
            if (user == null)
                return null;

            return user.Password != password ? null : CreateClaims(user.UserName);
        }
        /// <summary>
        /// Creates the claims.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public ClaimsIdentity CreateClaims(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
            };
            var claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
        /// <summary>
        /// Generates the token.
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <returns></returns>
        public AppUserModel GenerateToken(ClaimsIdentity identity)
        {
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.Issuer,
                audience: AuthOptions.Audience,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.Lifetime)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new AppUserModel
            {
                Token = encodedJwt,
                UserName = identity.Name
            };
        }
    }
}
