using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MonkeyShelter.Data.Model;

namespace MonkeyShelter.Tests.Data
{
    public class MonkeyMemoryRepository : IMonkeyRepository
    {
        public MonkeyMemoryRepository()
        {
            Monkeys = new List<Monkey>();
        }

        public MonkeyMemoryRepository(IEnumerable<Monkey> monkeys)
        {
            Monkeys = new List<Monkey>(monkeys);
        }

        public List<Monkey> Monkeys { get; }

        public ValueTask<IReadOnlyCollection<Monkey>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return ValueTask.FromResult((IReadOnlyCollection<Monkey>)Monkeys.AsReadOnly());
        }

        public ValueTask<Monkey> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            return ValueTask.FromResult(Monkeys.Find(x => x.Id == id));
        }

        public ValueTask<bool> AddAsync(Monkey monkey, CancellationToken cancellationToken = default)
        {
            if (Monkeys.Any(x => x.Id == monkey.Id))
                return ValueTask.FromResult(false);

            Monkeys.Add(monkey);
            return ValueTask.FromResult(true);
        }

        public ValueTask<bool> UpdateAsync(Monkey monkey, CancellationToken cancellationToken = default)
        {
            var old = Monkeys.Find(x => x.Id == monkey.Id);
            if (old == null)
                return ValueTask.FromResult(false);

            Monkeys.Remove(old);
            Monkeys.Add(monkey);
            return ValueTask.FromResult(true);
        }

        public ValueTask<bool> RemoveByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            var old = Monkeys.Find(x => x.Id == id);
            if (old == null)
                return ValueTask.FromResult(false);

            Monkeys.Remove(old);
            return ValueTask.FromResult(true);
        }
    }
}