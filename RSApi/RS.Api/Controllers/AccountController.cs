using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RS.Api.Controllers.Base;
using RS.Common.Extensions;
using RS.Domain.Models.Data;
using RS.Domain.Models.Filters;
using RS.Domain.Models.Views;
using RS.Services.Contracts;

namespace RS.Api.Controllers
{
    /// <summary>
    /// The account controller.
    /// </summary>
    public class AccountController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="serviceFactory">The service factory.</param>
        public AccountController(IServiceFactory serviceFactory)
            : base(serviceFactory) { }

        /// <summary>
        /// Tokens the specified user model.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        [HttpPost("/token")]
        [AllowAnonymous]
        public IActionResult Token(string email, string password)
        {
            if (!email.IsValidEmail())
                return BadRequest("Invalid email");

            if (string.IsNullOrEmpty(password))
                return BadRequest("Password is Empty");

            var identity = ServiceFactory.AccountService.GetIdentity(email, password);
            if (identity == null)
                return BadRequest("Invalid email or password.");

            var response = ServiceFactory.AccountService.GenerateToken(identity);
            return Json(response);
        }

        /// <summary>
        /// Gets the current user.
        /// </summary>
        /// <returns></returns>
        [HttpGet("/account/current")]
        public IActionResult GetCurrentUser()
        {
            var userName = User.Identity.Name;
            var identity = ServiceFactory.AccountService.CreateClaims(userName);
            var response = ServiceFactory.AccountService.GenerateToken(identity);
            return Json(response);
        }

        /// <summary>
        /// Registers the specified user model.
        /// </summary>
        /// <param name="appUserModel">The user model.</param>
        /// <returns></returns>
        [HttpPost("/account/register")]
        [AllowAnonymous]
        public IActionResult Register([FromBody]AppUserModel appUserModel)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage);

                return BadRequest(errors);
            }

            if (ServiceFactory.AppUserService.GetBy(new { appUserModel.Email }) != null)
            {
                return BadRequest("User already exists.");
            }

            appUserModel.UserName = appUserModel.Email;

            ServiceFactory.AppUserService.Create<Guid, AppUser>(ServiceFactory.Mapper.Map<AppUser>(appUserModel));

            var identity = ServiceFactory.AccountService.GetIdentity(appUserModel.UserName, appUserModel.Password);
            var response = ServiceFactory.AccountService.GenerateToken(identity);

            return Json(response);
        }

        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <returns></returns>
        [HttpGet("/account/{userName}")]
        [AllowAnonymous]
        public IActionResult GetUsers(string userName)
        {
            var userFilter = new AppUserFilter
            {
                UserName = userName
            };

            var user = ServiceFactory.AppUserService.GetUser(userFilter);
            return Json(user.RoleName);
        }

        /// <summary>
        /// Gets the users include orders.
        /// </summary>
        /// <returns></returns>
        //// todo change to /users?fields=orders
        [HttpGet("/account/orders")]
        [AllowAnonymous]
        public IActionResult GetUsersIncludeOrders()
        {
            var users = ServiceFactory.AppUserService.GetUsersIncludeOrders();
            return Json(users);
        }
    }
}
