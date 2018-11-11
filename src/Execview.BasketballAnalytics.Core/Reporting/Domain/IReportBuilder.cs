using System.Collections.Generic;
using Execview.BasketballAnalytics.Core.Domain;

namespace Execview.BasketballAnalytics.Core.Reporting.Domain
{
    public interface IReportBuilder
    {
        IReportBuilder WithPlayers(List<Player> players);
        IReportBuilder WithAveragePpg(decimal averagePpg);
        IReportBuilder WithAverageHeight(decimal averageHeight);
        IReportBuilder WithAchievement(Medal medal, Player player);
        Report Build();
    }
}