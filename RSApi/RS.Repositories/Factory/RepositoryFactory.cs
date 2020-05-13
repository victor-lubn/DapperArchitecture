using System;
using RS.Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using RS.Repositories.Contracts;
using RS.Repositories.Factory.Contracts;

namespace RS.Repositories.Factory
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly IServiceProvider provider;

        /// <summary>
        /// Gets the repository.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IDbRepository<T> GetRepository<T>() where T : BaseDbModel
        {
            return provider.GetService<IDbRepository<T>>();
        }

        public RepositoryFactory(IServiceProvider provider)
        {
            this.provider = provider;
        }
    }
}
