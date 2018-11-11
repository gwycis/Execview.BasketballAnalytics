using System;
using System.Collections.Generic;
using System.Linq;
using Execview.BasketballAnalytics.Core.Domain;
using Execview.BasketballAnalytics.Core.Reporting.Domain;

namespace Execview.BasketballAnalytics.Core.Reporting.Enrichers
{
    public sealed class AveragePlayerHeightEnricher : IReportEnricher
    {
        private readonly IImperialToMetricConverter _converter;

        public AveragePlayerHeightEnricher(IImperialToMetricConverter converter)
        {
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        public IReportBuilder EnrichReport(IEnumerable<Player> players, IReportBuilder partialReport)
        {
            if (players == null) throw new ArgumentNullException(nameof(players));
            if (partialReport == null) throw new ArgumentNullException(nameof(partialReport));

            var averageHeight = players.Select(p => ConvertPlayerHeightToCm(p.Height)).Average();
            return partialReport.WithAverageHeight(averageHeight);
        }

        private decimal ConvertPlayerHeightToCm(string height)
        {
            // '6 ft 2 in' => '6  2 in' => '6  2 ' => ["6", " ", " ", "2", " "] => ["6", "2"]
            var heightParts = height.Replace("ft", string.Empty).Replace("in", string.Empty)
                .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

            var feet = int.Parse(heightParts[0]);
            var inches = int.Parse(heightParts[1]);

            return _converter.ConvertToCm(feet, inches);
        }
    }
}