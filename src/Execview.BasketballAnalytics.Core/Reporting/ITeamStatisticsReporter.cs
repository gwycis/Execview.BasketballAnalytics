using System.Collections.Generic;
using Execview.BasketballAnalytics.Core.Domain;
using Execview.BasketballAnalytics.Core.Reporting.Domain;

namespace Execview.BasketballAnalytics.Core.Reporting
{
    public interface ITeamStatisticsReporter
    {
        Report GetReport(IEnumerable<Player> players);
    }
}