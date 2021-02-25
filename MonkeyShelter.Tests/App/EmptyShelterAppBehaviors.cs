using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Bogus;

using MonkeyShelter.App;
using MonkeyShelter.App.Model;
using MonkeyShelter.Data.Model;
using MonkeyShelter.Tests.Data;

using Xunit;

namespace MonkeyShelter.Tests.App
{
    public class EmptyShelterAppBehaviors
    {
        private readonly MonkeyMemoryRepository _repository;
        private readonly IShelterApp _target;
        private readonly Faker<Monkey> _monkeyGenerator;
        private readonly MonkeyComparer _monkeyComparer;

        public EmptyShelterAppBehaviors()
        {
            _repository = new MonkeyMemoryRepository();
            _target = new ShelterApp(_repository);
            _monkeyGenerator = new Faker<Monkey>()
                .RuleFor(x => x.Id, f => f.Random.String())
                .RuleFor(x => x.Name, f => f.Random.String())
                .RuleFor(x => x.Age, f => f.Random.Int())
                .RuleFor(x => x.Weight, f => f.Random.Int())
                .RuleFor(x => x.EyeColor, f => f.Random.String())
                .RuleFor(x => x.Species, f => f.Random.String())
                .RuleFor(x => x.Registered, f => f.Date.Recent())
                .RuleFor(x => x.FavoriteFruit, f => f.Random.String())
                ;

            _monkeyComparer = new MonkeyComparer();
        }

        [Fact]
        public async Task Registry_ZeroTotalMonkey()
        {
            var registry = await _target.GetRegistryAsync();

            Assert.Equal(0, registry.Total);
        }

        [Fact]
        public async Task Registry_EmptyMonkeyList()
        {
            var registry = await _target.GetRegistryAsync();

            Assert.Empty(registry.Monkeys);
        }

        [Fact]
        public async Task MonkeyDetails_ArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _target.GetMonkeyDetailsAsync(null));
        }

        [Fact]
        public async Task MonkeyDetails_NotFoundException()
        {
            var expected = _monkeyGenerator.Generate();
            await Assert.ThrowsAsync<NotFoundException>(() => _target.GetMonkeyDetailsAsync(expected.Id));
        }

        [Fact]
        public async Task RegisterMonkey_AddToRepository()
        {
            var expected = _monkeyGenerator.Generate();
            var request = new RegisterMonkeyRequest()
            {
                Id = expected.Id,
                Name = expected.Name,
                Age = expected.Age,
                Weight = expected.Weight,
                EyeColor = expected.EyeColor,
                Species = expected.Species,
                Registered = expected.Registered,
                FavoriteFruit = expected.FavoriteFruit,
            };
            await _target.RegisterMonkeyAsync(request);

            var actual = _repository.Monkeys.Single();

            Assert.Equal(expected, actual, _monkeyComparer);
        }

        [Fact]
        public async Task RegisterMonkey_NullRequest_ArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _target.RegisterMonkeyAsync(null));
        }

        [Fact]
        public async Task RegisterMonkey_NullId_ArgumentException()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _target.RegisterMonkeyAsync(new RegisterMonkeyRequest() { Id = null }));
        }

        [Fact]
        public async Task UpdateMonkey_NotFoundException()
        {
            var expected = _monkeyGenerator.Generate();
            var request = new UpdateRegistryRequest()
            {
                Id = expected.Id,
                Name = expected.Name,
                Age = expected.Age,
                Weight = expected.Weight,
                EyeColor = expected.EyeColor,
                Species = expected.Species,
                Registered = expected.Registered,
                FavoriteFruit = expected.FavoriteFruit,
            };
            await Assert.ThrowsAsync<NotFoundException>(() => _target.UpdateRegistryAsync(request));
        }

        [Fact]
        public async Task ReleaseMonkey_ArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _target.ReleaseMonkeyAsync(null));
        }

        [Fact]
        public async Task ReleaseMonkey_NotFoundException()
        {
            var expected = _monkeyGenerator.Generate();
            await Assert.ThrowsAsync<NotFoundException>(() => _target.ReleaseMonkeyAsync(expected.Id));
        }
    }
}
