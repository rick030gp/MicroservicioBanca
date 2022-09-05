using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroservicioBanca.Domain
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetById(TKey id);
        Task<TEntity> InsertAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity> UpdateAggregateAsync(TEntity entity);
        Task RemoveAsync(TEntity entity);
    }
}
