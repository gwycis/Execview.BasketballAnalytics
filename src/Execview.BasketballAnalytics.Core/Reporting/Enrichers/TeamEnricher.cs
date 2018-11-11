using System;
using System.Collections.Generic;
using System.Linq;
using Execview.BasketballAnalytics.Core.Domain;
using Execview.BasketballAnalytics.Core.Reporting.Domain;

namespace Execview.BasketballAnalytics.Core.Reporting.Enrichers
{
    public sealed class TeamEnricher : IReportEnricher
    {
        public IReportBuilder EnrichReport(IEnumerable<Player> players, IReportBuilder partialReport)
        {
            if (players == null) throw new ArgumentNullException(nameof(players));
            if (partialReport == null) throw new ArgumentNullException(nameof(partialReport));

            var sortedByPpg = players.OrderByDescending(p => p.PointsPerGame).ToList();
            return partialReport.WithPlayers(sortedByPpg);
        }
    }
}
