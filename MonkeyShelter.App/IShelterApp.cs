using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MonkeyShelter.Data.Model;

namespace MonkeyShelter.App
{
    public interface IShelterApp
    {
        Task<IReadOnlyCollection<Monkey>> ListRegistryAsync(CancellationToken cancellationToken = default);
        Task<Monkey> GetMonkeyDetailsAsync(string id, CancellationToken cancellationToken = default);
        Task RegisterMonkeyAsync(Monkey monkey, CancellationToken cancellationToken = default);
        Task UpdateRegistryAsync(Monkey monkey, CancellationToken cancellationToken = default);
        Task ReleaseMonkeyAsync(string id, CancellationToken cancellationToken = default);
    }
}