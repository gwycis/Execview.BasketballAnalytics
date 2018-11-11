using System.Collections.Generic;
using Execview.BasketballAnalytics.Core.Domain;
using Execview.BasketballAnalytics.Core.Reporting;
using Execview.BasketballAnalytics.Core.Reporting.Domain;
using NSubstitute;
using Xunit;

namespace Execview.BasketballAnalytics.Core.UnitTests.Reporting
{
    public class TeamStatisticsReporterTests
    {
        [Fact]
        public void Given_PlayersData_When_ReportRequested_Then_ReturnReportWithTeamStatistics()
        {
            // Arrange
            var enricher1 = Substitute.For<IReportEnricher>();
            var enricher2 = Substitute.For<IReportEnricher>();

            var sut = new TeamStatisticsReporter(new List<IReportEnricher>
            {
                enricher1,
                enricher2
            });

            var players = new List<Player> { };

            enricher1.EnrichReport(Arg.Any<IEnumerable<Player>>(), Arg.Any<IReportBuilder>())
                .Returns(ci => ci.Arg<IReportBuilder>());

            enricher2.EnrichReport(Arg.Any<IEnumerable<Player>>(), Arg.Any<IReportBuilder>())
                .Returns(ci => ci.Arg<IReportBuilder>());
            
            // Act
            var result = sut.GetReport(players);

            // Assert
            Assert.NotNull(result);
            Received.InOrder(() =>
                {
                    enricher1
                        .Received(1)
                        .EnrichReport(Arg.Is<IEnumerable<Player>>(p => p.Equals(players)),
                        Arg.Any<IReportBuilder>());

                    enricher2
                        .Received(1)
                        .EnrichReport(Arg.Is<IEnumerable<Player>>(p => p.Equals(players)),
                        Arg.Any<IReportBuilder>());
                });
        }
    }
}