using System.Collections.Generic;
using Execview.BasketballAnalytics.Core.Domain;
using Execview.BasketballAnalytics.Core.Reporting.Domain;

namespace Execview.BasketballAnalytics.Core.Reporting
{
    public interface IReportEnricher
    {
        IReportBuilder EnrichReport(IEnumerable<Player> players, IReportBuilder partialReport);
    }
}