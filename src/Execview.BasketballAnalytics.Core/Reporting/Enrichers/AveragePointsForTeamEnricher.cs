using System;
using System.Collections.Generic;
using System.Linq;
using Execview.BasketballAnalytics.Core.Domain;
using Execview.BasketballAnalytics.Core.Reporting.Domain;

namespace Execview.BasketballAnalytics.Core.Reporting.Enrichers
{
    public sealed class AveragePointsForTeamEnricher : IReportEnricher
    {
        public IReportBuilder EnrichReport(IEnumerable<Player> players, IReportBuilder partialReport)
        {
            if (players == null) throw new ArgumentNullException(nameof(players));
            if (partialReport == null) throw new ArgumentNullException(nameof(partialReport));

            var averagePoints = Math.Round(players.Average(p => p.PointsPerGame), 2);
            return partialReport.WithAveragePpg(averagePoints);
        }
    }
}
