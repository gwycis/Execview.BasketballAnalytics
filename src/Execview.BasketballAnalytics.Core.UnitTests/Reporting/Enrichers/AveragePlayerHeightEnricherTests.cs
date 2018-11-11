using System.Collections.Generic;
using Execview.BasketballAnalytics.Core.Domain;
using Execview.BasketballAnalytics.Core.Reporting.Domain;
using Execview.BasketballAnalytics.Core.Reporting.Enrichers;
using NSubstitute;
using Xunit;

namespace Execview.BasketballAnalytics.Core.UnitTests.Reporting.Enrichers
{
    public sealed class AveragePlayerHeightEnricherTests
    {
        private static Player CreatePlayer(string height)
        {
            return new Player(1, Position.PG, 10, "United States", "Jordan, Michael", height, "175 lb", "Iowa",
                32.6m);
        }

        [Fact]
        public void Given_AllPlayers_When_EnrichingReport_Then_ReturnPlayersAverageHeight()
        {
            // Assert
            var converter = Substitute.For<IImperialToMetricConverter>();
            var sut = new AveragePlayerHeightEnricher(converter);
            var builder = Substitute.For<IReportBuilder>();
            var players = new List<Player>
            {
                CreatePlayer("6 ft 2 in"),  // 188 cm
                CreatePlayer("6 ft 10 in"), // 208 cm
                CreatePlayer("7 ft 1 in")   // 216 cm
            };

            converter.ConvertToCm(Arg.Is<int>(v => v == 6), Arg.Is<int>(v => v == 2)).Returns(188);
            converter.ConvertToCm(Arg.Is<int>(v => v == 6), Arg.Is<int>(v => v == 10)).Returns(208);
            converter.ConvertToCm(Arg.Is<int>(v => v == 7), Arg.Is<int>(v => v == 1)).Returns(216);

            var result = 0m;
            builder.WithAverageHeight(Arg.Do<decimal>(p => result = p));

            // Act
            sut.EnrichReport(players, builder);

            // Arrange
            converter.Received(1).ConvertToCm(Arg.Is<int>(v => v == 6), Arg.Is<int>(v => v == 2));
            converter.Received(1).ConvertToCm(Arg.Is<int>(v => v == 6), Arg.Is<int>(v => v == 10));
            converter.Received(1).ConvertToCm(Arg.Is<int>(v => v == 7), Arg.Is<int>(v => v == 1));

            Assert.Equal(204.00m, result);
        }
    }
}