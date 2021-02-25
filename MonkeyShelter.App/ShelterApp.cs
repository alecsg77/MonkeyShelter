using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MonkeyShelter.Data.Model;

namespace MonkeyShelter.App
{
    public class ShelterApp : IShelterApp
    {
        private readonly IMonkeyRepository _repository;

        public ShelterApp(IMonkeyRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyCollection<Monkey>> ListRegistryAsync(CancellationToken cancellationToken = default)
        {
            return await _repository.GetAllAsync(cancellationToken);
        }

        public async Task<Monkey> GetMonkeyDetailsAsync(string id, CancellationToken cancellationToken = default)
        {
            var monkey = await _repository.GetByIdAsync(id, cancellationToken);
            if (monkey == null)
            {
                throw new NotFoundException(id);
            }
            return monkey;
        }

        public async Task RegisterMonkeyAsync(Monkey monkey, CancellationToken cancellationToken = default)
        {
            if (!await _repository.AddAsync(monkey, cancellationToken))
            {
                throw new ConflictException(monkey.Id);
            }
        }

        public async Task UpdateRegistryAsync(Monkey monkey, CancellationToken cancellationToken = default)
        {
            if (!await _repository.UpdateAsync(monkey, cancellationToken))
            {
                throw new NotFoundException(monkey.Id);
            }
        }

        public async Task ReleaseMonkeyAsync(string id, CancellationToken cancellationToken = default)
        {
            if (!await _repository.RemoveByIdAsync(id, cancellationToken))
            {
                throw new NotFoundException(id);
            }
        }
    }
}
