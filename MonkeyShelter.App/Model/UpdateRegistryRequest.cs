using System;

namespace MonkeyShelter.App.Model
{
    public class UpdateRegistryRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Weight { get; set; }
        public string EyeColor { get; set; }
        public string Species { get; set; }
        public DateTime Registered { get; set; }
        public string FavoriteFruit { get; set; }
    }
}