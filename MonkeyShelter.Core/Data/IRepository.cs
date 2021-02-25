using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MonkeyShelter.Data
{
    public interface IRepository<TEntity, in TKey> where TEntity : class, IEntity<TKey>
    {
        ValueTask<IReadOnlyCollection<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
        ValueTask<TEntity> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);
        ValueTask<bool> AddAsync(TEntity monkey, CancellationToken cancellationToken = default);
        ValueTask<bool> UpdateAsync(TEntity monkey, CancellationToken cancellationToken = default);
        ValueTask<bool> RemoveByIdAsync(TKey id, CancellationToken cancellationToken = default);
    }
}
