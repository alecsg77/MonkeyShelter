using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MonkeyShelter.App.Model;
using MonkeyShelter.Data.Model;

namespace MonkeyShelter.App
{
    public interface IShelterApp
    {
        Task<MonkeyRegistry> GetRegistryAsync(CancellationToken cancellationToken = default);
        Task<MonkeyDetails> GetMonkeyDetailsAsync(string id, CancellationToken cancellationToken = default);
        Task RegisterMonkeyAsync(RegisterMonkeyRequest request, CancellationToken cancellationToken = default);
        Task UpdateRegistryAsync(UpdateRegistryRequest request, CancellationToken cancellationToken = default);
        Task ReleaseMonkeyAsync(string id, CancellationToken cancellationToken = default);
    }
}
