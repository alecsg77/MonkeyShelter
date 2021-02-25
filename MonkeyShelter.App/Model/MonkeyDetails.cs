using System;
using System.Collections.Generic;
using System.Text;

using MonkeyShelter.Data.Model;

namespace MonkeyShelter.App.Model
{
    public class MonkeyDetails
    {
        public MonkeyDetails(Monkey monkey)
        {
            Id = monkey.Id;
            Name = monkey.Name;
            Age = monkey.Age;
            Weight = monkey.Weight;
            EyeColor = monkey.EyeColor;
            Species = monkey.Species;
            Registered = monkey.Registered;
            FavoriteFruit = monkey.FavoriteFruit;
        }

        public string Id { get; }
        public string Name { get; }
        public int Age { get; }
        public int Weight { get; }
        public string EyeColor { get; }
        public string Species { get; }
        public DateTime Registered { get; }
        public string FavoriteFruit { get; }
    }
}

