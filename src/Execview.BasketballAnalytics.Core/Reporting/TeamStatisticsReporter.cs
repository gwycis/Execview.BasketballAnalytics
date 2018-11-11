using System;
using System.Collections.Generic;
using Execview.BasketballAnalytics.Core.Domain;
using Execview.BasketballAnalytics.Core.Reporting.Domain;

namespace Execview.BasketballAnalytics.Core.Reporting
{
    public sealed class TeamStatisticsReporter : ITeamStatisticsReporter
    {
        private readonly IEnumerable<IReportEnricher> _enrichers;

        public TeamStatisticsReporter(IEnumerable<IReportEnricher> enrichers)
        {
            _enrichers = enrichers ?? throw new ArgumentNullException(nameof(enrichers));
        }
        
        public Report GetReport(IEnumerable<Player> players)
        {
            IReportBuilder builder = new ReportBuilder();

            foreach (var enricher in _enrichers)
            {
                builder = enricher.EnrichReport(players, builder);
            }

            return builder.Build();
        }
    }
}
