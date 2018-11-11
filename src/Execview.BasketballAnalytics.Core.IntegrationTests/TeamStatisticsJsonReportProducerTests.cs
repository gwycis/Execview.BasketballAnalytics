using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Execview.BasketballAnalytics.Core.Data;
using Execview.BasketballAnalytics.Core.Domain;
using Execview.BasketballAnalytics.Core.Reporting;
using Execview.BasketballAnalytics.Core.Reporting.Enrichers;
using Newtonsoft.Json;
using Xunit;

namespace Execview.BasketballAnalytics.Core.IntegrationTests
{
    public class TeamStatisticsJsonReportProducerTests
    {
        [Fact]
        public async Task Given_DataFile_When_Reporting_Then_ProduceJsonReport()
        {
            // Arrange
            var datafile = Path.Combine(AppContext.BaseDirectory, "Data", "chicago-bulls.csv");
            var expectedReportFile = Path.Combine(AppContext.BaseDirectory, "Data", "chicago-bulls.json");
            var originalReport = await File.ReadAllTextAsync(expectedReportFile);
            var reportObject = JsonConvert.DeserializeObject(originalReport);
            var expectedReport = JsonConvert.SerializeObject(reportObject, Formatting.Indented);

            var playerParser = new PlayerParser();
            var dataProvider = new CsvDataProvider(playerParser);
            var metricConverter = new ImperialToMetricConverter();
            var enrichers = new List<IReportEnricher>
            {
                new AveragePlayerHeightEnricher(metricConverter),
                new AveragePointsForTeamEnricher(),
                new TeamEnricher(),
                new TopAchieversEnricher()
            };

            var statisticsReporter = new TeamStatisticsReporter(enrichers);

            var sut = new TeamStatisticsJsonReporter(dataProvider, statisticsReporter);

            // Act
            var result = await sut.GetReportAsJsonAsync(datafile);

            // Assert
           Assert.Equal(expectedReport, result);
        }
    }
}