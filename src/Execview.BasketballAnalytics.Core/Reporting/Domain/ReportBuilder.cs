using System;
using System.Collections.Generic;
using System.Linq;
using Execview.BasketballAnalytics.Core.Domain;
using Execview.BasketballAnalytics.Core.Reporting.Domain.Achievements;

namespace Execview.BasketballAnalytics.Core.Reporting.Domain
{
    public sealed class ReportBuilder : IReportBuilder
    {
        private const int MinimumNumberOfPlayersRequired = 5; 

        public ReportBuilder()
        {
            Players = new List<Player>();
            Leaders = new Dictionary<Medal, Player>();
            PlayersPerPosition = new Dictionary<Position, int>
            {
                { Position.PG, 0},
                { Position.C, 0},
                { Position.PF, 0},
                { Position.SG, 0},
                { Position.SF, 0}
            };
        }

        public IEnumerable<Player> Players { get; private set; }
        public decimal AveragePpg { get; private set; }
        public Dictionary<Medal, Player> Leaders { get; }
        public Dictionary<Position, int> PlayersPerPosition { get; }
        public decimal AverageHeight { get; private set; }
        
        public IReportBuilder WithPlayers(List<Player> players)
        {
            if (players == null) throw new ArgumentNullException(nameof(players));
            if (players.Count < MinimumNumberOfPlayersRequired)
                throw  new ArgumentException($"Minimum number of players required is {MinimumNumberOfPlayersRequired}, " +
                                             $"but we have only {players.Count}");

            Players = players;

            foreach (var player in players)
            {
                PlayersPerPosition[player.Position] = PlayersPerPosition[player.Position] + 1;
            }

            return this;
        }

        public IReportBuilder WithAveragePpg(decimal averagePpg)
        {
            if (averagePpg <= 0) throw new ArgumentOutOfRangeException(nameof(averagePpg));
            AveragePpg = averagePpg;

            return this;
        }

        public IReportBuilder WithAverageHeight(decimal averageHeight)
        {
            if (averageHeight <= 0) throw new ArgumentOutOfRangeException(nameof(averageHeight));
            AverageHeight = averageHeight;

            return this;
        }

        public IReportBuilder WithAchievement(Medal medal, Player player)
        {
            if (player == null) throw new ArgumentNullException(nameof(player));
            if (Leaders.ContainsKey(medal))
                throw new ArgumentException($"Player for '{Enum.GetName(typeof(Medal), medal)}' has already been assigned. " +
                                             "Only 1 player per achievement is allowed.");

            if (Leaders.ContainsValue(player))
                throw new ArgumentException($"Player '{player.Name}' already has a medal! " +
                                             "Only 1 medal per player is allowed.");

            Leaders.Add(medal, player);
            return this;
        }

        public Report Build()
        {
            var leaders = Leaders.Select(l => CreateAchievement(l.Key, l.Value)).ToList();
            var averageHeightInCm = $"{Math.Round(AverageHeight, 1)} cm";
            return new Report(Players, AveragePpg, leaders, PlayersPerPosition, averageHeightInCm);
        }

        private Achievement CreateAchievement(Medal medal, Player player)
        {
            switch (medal)
            {
                case Medal.Gold:
                    return new GoldMedalAchievement(player.Name, player.PointsPerGame);
                case Medal.Silver:
                    return new SilverMedalAchievement(player.Name, player.PointsPerGame);
                case Medal.Bronze:
                    return new BronzeMedalAchievement(player.Name, player.PointsPerGame);
                default:
                    throw new ArgumentException(
                        $"'{Enum.GetName(typeof(Medal), medal)}' achievement is not supported!");
            }
        }
    }
}