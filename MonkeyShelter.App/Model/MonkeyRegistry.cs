using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MonkeyShelter.Data.Model;

namespace MonkeyShelter.App.Model
{
    public class MonkeyRegistry
    {
        public MonkeyRegistry(IReadOnlyCollection<Monkey> monkeys)
        {
            Total = monkeys.Count;
            Monkeys = monkeys.Select(x => new MonkeyDetails(x)).ToList();
        }

        public int Total { get; }
        public IEnumerable<MonkeyDetails> Monkeys { get; }
    }
}
