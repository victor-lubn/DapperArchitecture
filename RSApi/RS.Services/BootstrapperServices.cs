using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RS.Repositories;
using RS.Services.Azure;
using RS.Services.Azure.Contracts;
using RS.Services.Common;
using RS.Services.Common.Contracts;
using RS.Services.Contracts;
using RS.Services.Data;
using RS.Services.Data.Contracts;
using RS.Services.Mapping;

namespace RS.Services
{
    /// <summary>
    /// The BootstrapperServices
    /// </summary>
    public static class BootstrapperServices
    {
        /// <summary>
        /// Initializes the specified services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        public static void Initialize(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IServiceFactory, ServiceFactory>(provider => new ServiceFactory(provider));
            services.AddScoped<IAzureStorageService, AzureStorageService>();
            services.AddScoped<IAppUserService, AppUserService>();
            services.AddScoped<IAccountService, AccountService>();
            
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            BootstrapperRepositiries.Initialize(services, configuration);
        }
    }
}
