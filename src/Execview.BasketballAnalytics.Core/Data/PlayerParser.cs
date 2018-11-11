using System;
using Execview.BasketballAnalytics.Core.Domain;

namespace Execview.BasketballAnalytics.Core.Data
{
    public sealed class PlayerParser : IPlayerParser
    {
        private const char Separator = ',';
        private const int ExpectedDataPointsCount = 10;

        public Player Build(string data)
        {
            var parts = data.Split(Separator);
            if (parts.Length != ExpectedDataPointsCount)
                throw new ArgumentException($"Value is expected to have '{ExpectedDataPointsCount}' " +
                                            $"data points separated by '{Separator}', but the passed " +
                                            $"value has '{parts.Length}' instead!");

            var id = int.Parse(parts[0]);
            var position = (Position)Enum.Parse(typeof(Position), parts[1]);
            var number = int.Parse(parts[2]);
            var country = parts[3];
            var name = ParseName(parts[4], parts[5]);
            var height = parts[6];
            var weight = parts[7];
            var university = parts[8];
            var ppg = decimal.Parse(parts[9]);

            return new Player(id, position, number, country, name, height, weight, university, ppg);
        }

        private static string ParseName(string lastName, string firstName)
        {
            return $"{lastName.Replace("\"", string.Empty).Trim()}, {firstName.Replace("\"", string.Empty).Trim()}";
        }
    }
}