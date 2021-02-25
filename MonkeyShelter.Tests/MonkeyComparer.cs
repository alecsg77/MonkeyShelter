using System;
using System.Collections.Generic;
using MonkeyShelter.Data.Model;

namespace MonkeyShelter.Tests
{
    public class MonkeyComparer : IEqualityComparer<Monkey>
    {
        public bool Equals(Monkey x, Monkey y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return string.Equals(x.Name, y.Name, StringComparison.InvariantCulture)
                   && x.Age == y.Age
                   && x.Weight == y.Weight
                   && string.Equals(x.EyeColor, y.EyeColor, StringComparison.InvariantCulture)
                   && string.Equals(x.Species, y.Species, StringComparison.InvariantCulture)
                   && x.Registered.Equals(y.Registered)
                   && string.Equals(x.FavoriteFruit, y.FavoriteFruit, StringComparison.InvariantCulture);
        }

        public int GetHashCode(Monkey obj)
        {
            var hashCode = new HashCode();
            hashCode.Add(obj.Name, StringComparer.InvariantCulture);
            hashCode.Add(obj.Age);
            hashCode.Add(obj.Weight);
            hashCode.Add(obj.EyeColor, StringComparer.InvariantCulture);
            hashCode.Add(obj.Species, StringComparer.InvariantCulture);
            hashCode.Add(obj.Registered);
            hashCode.Add(obj.FavoriteFruit, StringComparer.InvariantCulture);
            return hashCode.ToHashCode();
        }
    }
}