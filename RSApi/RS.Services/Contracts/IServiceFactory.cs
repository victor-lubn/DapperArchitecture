using AutoMapper;
using Microsoft.Extensions.Configuration;
using RS.Services.Azure.Contracts;
using RS.Services.Common.Contracts;
using RS.Services.Data.Contracts;

namespace RS.Services.Contracts
{
    /// <summary>
    /// The ServiceFactory
    /// </summary>
    public interface IServiceFactory
    {
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        IConfiguration Configuration { get; }
        /// <summary>
        /// Gets the users service.
        /// </summary>
        /// <value>
        /// The users service.
        /// </value>
        IAppUserService AppUserService { get; }
        /// <summary>
        /// Gets the azure storage service.
        /// </summary>
        /// <value>
        /// The azure storage service.
        /// </value>
        IAzureStorageService AzureStorageService { get; }
        /// <summary>
        /// Gets the account service.
        /// </summary>
        /// <value>
        /// The account service.
        /// </value>
        IAccountService AccountService { get; }
        /// <summary>
        /// Gets the mapper.
        /// </summary>
        /// <value>
        /// The mapper.
        /// </value>
        IMapper Mapper { get; }
    }
}