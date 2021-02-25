using System;
using System.Threading;
using System.Threading.Tasks;
using MonkeyShelter.App.Model;
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

        public async Task<MonkeyRegistry> GetRegistryAsync(CancellationToken cancellationToken = default)
        {
            var monkeys = await _repository.GetAllAsync(cancellationToken);
            return new MonkeyRegistry(monkeys);
        }

        public async Task<MonkeyDetails> GetMonkeyDetailsAsync(string id, CancellationToken cancellationToken = default)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            var monkey = await _repository.GetByIdAsync(id, cancellationToken);
            if (monkey == null)
            {
                throw new NotFoundException(id);
            }
            return new MonkeyDetails(monkey);
        }

        public async Task RegisterMonkeyAsync(RegisterMonkeyRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.Id == null) throw new ArgumentException("Id null", nameof(request));
            
            var monkey = new Monkey()
            {
                Id = request.Id,
                Name = request.Name,
                Age = request.Age,
                Weight = request.Weight,
                EyeColor = request.EyeColor,
                Species = request.Species,
                Registered = request.Registered,
                FavoriteFruit = request.FavoriteFruit,
            };

            if (!await _repository.AddAsync(monkey, cancellationToken))
            {
                throw new ConflictException(request.Id);
            }
        }

        public async Task UpdateRegistryAsync(UpdateRegistryRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.Id == null) throw new ArgumentException("Id null", nameof(request));

            var monkey = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (monkey == null)
            {
                throw new NotFoundException(request.Id);
            }

            monkey.Name = request.Name;
            monkey.Age = request.Age;
            monkey.Weight = request.Weight;
            monkey.EyeColor = request.EyeColor;
            monkey.Species = request.Species;
            monkey.Registered = request.Registered;
            monkey.FavoriteFruit = request.FavoriteFruit;

            if (!await _repository.UpdateAsync(monkey, cancellationToken))
            {
                throw new NotFoundException(request.Id);
            }
        }

        public async Task ReleaseMonkeyAsync(string id, CancellationToken cancellationToken = default)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            if (!await _repository.RemoveByIdAsync(id, cancellationToken))
            {
                throw new NotFoundException(id);
            }
        }
    }
}
