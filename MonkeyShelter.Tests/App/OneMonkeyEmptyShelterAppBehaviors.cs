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
    public class OneMonkeyEmptyShelterAppBehaviors
    {
        private readonly MonkeyMemoryRepository _repository;
        private readonly IShelterApp _target;
        private readonly Faker<Monkey> _monkeyGenerator;
        private readonly MonkeyComparer _monkeyComparer;
        private readonly Monkey _singleMonkey;

        public OneMonkeyEmptyShelterAppBehaviors()
        {
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
            _singleMonkey = _monkeyGenerator.Generate();
            _repository = new MonkeyMemoryRepository(new[] { _singleMonkey });
            _target = new ShelterApp(_repository);
            _monkeyComparer = new MonkeyComparer();
        }

        private static bool EqualMonkeyDetails(Monkey expected, MonkeyDetails actual)
        {
            return string.Equals(expected.Name, actual.Name, StringComparison.InvariantCulture)
                   && expected.Age == actual.Age
                   && expected.Weight == actual.Weight
                   && string.Equals(expected.EyeColor, actual.EyeColor, StringComparison.InvariantCulture)
                   && string.Equals(expected.Species, actual.Species, StringComparison.InvariantCulture)
                   && expected.Registered.Equals(actual.Registered)
                   && string.Equals(expected.FavoriteFruit, actual.FavoriteFruit, StringComparison.InvariantCulture);
        }

        [Fact]
        public async Task Registry_OneTotalMonkey()
        {
            var registry = await _target.GetRegistryAsync();

            Assert.Equal(1, registry.Total);
        }

        [Fact]
        public async Task Registry_SingleMonkeyList()
        {
            var expected = _singleMonkey;
            var registry = await _target.GetRegistryAsync();

            var actual = registry.Monkeys.Single();
            Assert.True(EqualMonkeyDetails(expected, actual));
        }

        [Fact]
        public async Task MonkeyDetails_SingleMonkey()
        {
            var expected = _singleMonkey;
            var actual = await _target.GetMonkeyDetailsAsync(expected.Id);
            Assert.True(EqualMonkeyDetails(expected, actual));
        }

        [Fact]
        public async Task RegisterMonkey_AddToRepository()
        {
            var newMonkey = _monkeyGenerator.Generate();
            var expected = new[]{_singleMonkey, newMonkey};

            var request = new RegisterMonkeyRequest()
            {
                Id = newMonkey.Id,
                Name = newMonkey.Name,
                Age = newMonkey.Age,
                Weight = newMonkey.Weight,
                EyeColor = newMonkey.EyeColor,
                Species = newMonkey.Species,
                Registered = newMonkey.Registered,
                FavoriteFruit = newMonkey.FavoriteFruit,
            };
            await _target.RegisterMonkeyAsync(request);

            var actual = _repository.Monkeys;

            Assert.Equal(expected, actual, _monkeyComparer);
        }

        [Fact]
        public async Task UpdateMonkey_UpdateRepository()
        {
            var expected = _monkeyGenerator.Generate();
            expected.Id = _singleMonkey.Id;

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
            await _target.UpdateRegistryAsync(request);

            var actual = _repository.Monkeys.Single();

            Assert.Equal(expected, actual, _monkeyComparer);
        }

        [Fact]
        public async Task ReleaseMonkey_RemoveFromRepository()
        {
            await _target.ReleaseMonkeyAsync(_singleMonkey.Id);

            Assert.Empty(_repository.Monkeys);
        }
    }
}
