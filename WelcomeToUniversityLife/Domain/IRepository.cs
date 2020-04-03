using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain
{
    public interface IRepository<TEntity, TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetAsync(int id);
        Task CreateAsync(TEntity item);
        TEntity Update(TEntity item);
        Task DeleteAsync(int id);
    }
}
