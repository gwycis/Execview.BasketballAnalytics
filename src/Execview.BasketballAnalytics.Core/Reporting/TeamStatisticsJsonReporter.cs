using System;
using System.Threading.Tasks;
using Execview.BasketballAnalytics.Core.Data;
using Newtonsoft.Json;

namespace Execview.BasketballAnalytics.Core.Reporting
{
   public sealed class TeamStatisticsJsonReporter
    {
        private readonly IFileDataProvider _dataProvider;
        private readonly ITeamStatisticsReporter _teamStatisticsReporter;

        public TeamStatisticsJsonReporter(IFileDataProvider dataProvider,
            ITeamStatisticsReporter teamStatisticsReporter)
        {
            _dataProvider = dataProvider ?? throw new ArgumentNullException(nameof(dataProvider));
            _teamStatisticsReporter =
                teamStatisticsReporter ?? throw new ArgumentNullException(nameof(teamStatisticsReporter));
        }

        public async Task<string> GetReportAsJsonAsync(string datafile)
        {
            var players = await _dataProvider.GetPlayersAsync(datafile);
            var report = _teamStatisticsReporter.GetReport(players);
            return JsonConvert.SerializeObject(report, Formatting.Indented);
        }
    }
}