using System;
using System.Collections.Generic;
using System.Linq;
using Execview.BasketballAnalytics.Core.Domain;
using Execview.BasketballAnalytics.Core.Reporting.Domain;

namespace Execview.BasketballAnalytics.Core.Reporting.Enrichers
{
    public sealed class TopAchieversEnricher : IReportEnricher
    {
        public IReportBuilder EnrichReport(IEnumerable<Player> players, IReportBuilder partialReport)
        {
            if (players == null) throw new ArgumentNullException(nameof(players));
            if (partialReport == null) throw new ArgumentNullException(nameof(partialReport));

            var bestPlayers = players.OrderByDescending(p => p.PointsPerGame).Take(3).ToList();
            return partialReport
                .WithAchievement(Medal.Gold, bestPlayers[0])
                .WithAchievement(Medal.Silver, bestPlayers[1])
                .WithAchievement(Medal.Bronze, bestPlayers[2]);
        }
    }
}
