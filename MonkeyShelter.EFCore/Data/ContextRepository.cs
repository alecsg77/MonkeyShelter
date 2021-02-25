using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MonkeyShelter.Data
{
    public class ContextRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity: class, IEntity<TKey>
    {
        private readonly ShelterContext _context;

        public ContextRepository(ShelterContext context)
        {
            _context = context;
        }

        public async ValueTask<IReadOnlyCollection<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Set<TEntity>().ToListAsync(cancellationToken);
        }

        public async ValueTask<TEntity> GetByIdAsync(TKey id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken);
        }

        public async ValueTask<bool> AddAsync(TEntity monkey, CancellationToken cancellationToken = default)
        {
            await _context.Set<TEntity>().AddAsync(monkey, cancellationToken);
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException)
            {
                if (EntityExists(monkey.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        public async ValueTask<bool> UpdateAsync(TEntity monkey, CancellationToken cancellationToken = default)
        {
            _context.Entry(monkey).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityExists(monkey.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        public async ValueTask<bool> RemoveByIdAsync(TKey id, CancellationToken cancellationToken = default)
        {
            var monkey = await _context.Set<TEntity>().FindAsync(id);
            if (monkey == null)
            {
                return false;
            }

            _context.Set<TEntity>().Remove(monkey);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        private bool EntityExists(TKey id)
        {
            return _context.Set<TEntity>().Any(e => Equals(e.Id, id));
        }
    }
}
