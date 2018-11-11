using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Execview.BasketballAnalytics.Core.Domain
{
    public sealed class Player
    {
        public Player(int id, Position position, int number, string country, string name, string height,
            string weight, string university, decimal pointsPerGame)
        {
            Id = id;
            Position = position;
            Number = number;
            Country = country;
            Name = name;
            Height = height;
            Weight = weight;
            University = university;
            PointsPerGame = pointsPerGame;
        }

        public int Id { get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Position Position { get; }

        public int Number { get; }
        public string Country { get; }
        public string Name { get; }
        public string Height { get; }
        public string Weight { get; }
        public string University { get; }

        [JsonProperty(PropertyName = "PPG")]
        public decimal PointsPerGame { get; }
    }
}