using System;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RS.Services.Azure.Contracts;
using RS.Services.Common.Contracts;
using RS.Services.Contracts;
using RS.Services.Data.Contracts;

namespace RS.Services
{
    /// <summary>
    /// The ServiceFactory
    /// </summary>
    /// <seealso cref="RS.Services.Contracts.IServiceFactory" />
    public class ServiceFactory : IServiceFactory
    {
        /// <summary>
        /// The provider
        /// </summary>
        private readonly IServiceProvider provider;
        /// <summary>
        /// Gets the users service.
        /// </summary>
        /// <value>
        /// The users service.
        /// </value>
        public IAppUserService AppUserService => GetService<IAppUserService>();
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IConfiguration Configuration => GetService<IConfiguration>();
        /// <summary>
        /// Gets the azure storage service.
        /// </summary>
        /// <value>
        /// The azure storage service.
        /// </value>
        public IAzureStorageService AzureStorageService => GetService<IAzureStorageService>();
        /// <summary>
        /// Gets the account service.
        /// </summary>
        /// <value>
        /// The account service.
        /// </value>
        public IAccountService AccountService => GetService<IAccountService>();
        /// <summary>
        /// Gets the mapper.
        /// </summary>
        /// <value>
        /// The mapper.
        /// </value>
        public IMapper Mapper => GetService<IMapper>();
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceFactory"/> class.
        /// </summary>
        /// <param name="provider">The provider.</param>
        public ServiceFactory(IServiceProvider provider)
        {
            this.provider = provider;
        }
        /// <summary>
        /// Gets the service.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private T GetService<T>()
        {
            return provider.GetService<T>();
        }
    }
}
