using RS.Domain.Models;
using RS.Repositories.Contracts;

namespace RS.Repositories.Factory.Contracts
{
    public interface IRepositoryFactory
    {
        IDbRepository<T> GetRepository<T>() where T : BaseDbModel;
    }
}
