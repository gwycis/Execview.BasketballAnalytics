using System.Collections.Generic;
using Execview.BasketballAnalytics.Core.Domain;
using Execview.BasketballAnalytics.Core.Reporting.Domain;
using Execview.BasketballAnalytics.Core.Reporting.Enrichers;
using NSubstitute;
using Xunit;

namespace Execview.BasketballAnalytics.Core.UnitTests.Reporting.Enrichers
{
    public sealed class AveragePointsForTeamEnricherTests
    {
        private static Player CreatePlayer(decimal ppg)
        {
            return new Player(1, Position.PG, 10, "United States", "Armstrong, B.J.", "6 ft 2 in", "175 lb", "Iowa",
                ppg);
        }

        [Theory]
        [InlineData(10, 12)]
        [InlineData(10.333, 12.07)]
        public void Given_AllPlayers_When_EnrichingReport_Then_ReturnAverageTeamPpg(decimal ppg, decimal expectedAverage)
        {
            // Assert
            var sut = new AveragePointsForTeamEnricher();
            var builder = Substitute.For<IReportBuilder>();
            var players = new List<Player>()
            {
                CreatePlayer(ppg),
                CreatePlayer(15),
                CreatePlayer(10),
                CreatePlayer(20),
                CreatePlayer(5)
            };

            var result = 0m;
            builder.WithAveragePpg(Arg.Do<decimal>(p => result = p));
      
            // Act
            sut.EnrichReport(players, builder);

            // Arrange
            Assert.Equal(expectedAverage, result);
        }
    }
}