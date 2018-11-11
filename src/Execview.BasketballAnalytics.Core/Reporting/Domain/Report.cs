using System;
using System.Collections.Generic;
using Execview.BasketballAnalytics.Core.Domain;
using Execview.BasketballAnalytics.Core.Reporting.Domain.Achievements;
using Newtonsoft.Json;

namespace Execview.BasketballAnalytics.Core.Reporting.Domain
{
    public sealed class Report
    {
        public Report(IEnumerable<Player> players, decimal averagePpg, IEnumerable<Achievement> leaders,
            Dictionary<Position, int> playersPerPosition, string averageHeight)
        {
            Players = players ?? throw new ArgumentNullException(nameof(players));
            AveragePpg = averagePpg;
            Leaders = leaders ?? throw new ArgumentNullException(nameof(leaders));
            PlayersPerPosition = playersPerPosition ?? throw new ArgumentNullException(nameof(playersPerPosition));
            AverageHeight = averageHeight;
        }

        public IEnumerable<Player> Players { get; }

        [JsonProperty(PropertyName = "AveragePPG")]
        public decimal AveragePpg { get; }
        public IEnumerable<Achievement> Leaders { get; }

        [JsonProperty(PropertyName = "")]
        public Dictionary<Position, int> PlayersPerPosition { get; }

        public string AverageHeight { get; }
    }
}