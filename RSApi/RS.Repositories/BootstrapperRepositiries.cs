using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RS.Domain.Models;
using RS.Repositories.Contracts;
using RS.Repositories.Factory;
using RS.Repositories.QueryBuilders.Factory;
using RS.Repositories.QueryBuilders.Factory.Contracts;
using RS.Repositories.Factory.Contracts;
using RS.Repositories.QueryBuilders.Contracts;
using RS.Repositories.QueryBuilders;
using System.Data.SQLite;

namespace RS.Repositories
{
    /// <summary>
    /// The BootstrapperRepositiries
    /// </summary>
    public class BootstrapperRepositiries
    {
        /// <summary>
        /// Initializes the specified services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        public static void Initialize(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDbConnection>(provider => new SQLiteConnection(configuration["DBConnectionString"]));
            services.AddScoped<IRepositoryFactory, RepositoryFactory>(provider => new RepositoryFactory(provider));
            services.AddScoped<IConnectionFactory, ConnectionFactory>();
            services.AddScoped<IDbRepository<BaseDbModel>, DbRepository<BaseDbModel>>();
            services.AddScoped<IDapperRepository, DapperRepository>();
            services.AddScoped(typeof(IDbRepository<>), typeof(DbRepository<>));
            services.AddScoped<IQueryBuilderFactory, QueryBuilderFactory>(provider => new QueryBuilderFactory(provider));

            services.AddScoped<IAppUserQueryBuilder, AppUserQueryBuilder>();
        }
    }
}
