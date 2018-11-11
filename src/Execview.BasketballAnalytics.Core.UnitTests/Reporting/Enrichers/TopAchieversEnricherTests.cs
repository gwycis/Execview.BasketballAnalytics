using System.Collections.Generic;
using Execview.BasketballAnalytics.Core.Domain;
using Execview.BasketballAnalytics.Core.Reporting.Domain;
using Execview.BasketballAnalytics.Core.Reporting.Enrichers;
using NSubstitute;
using Xunit;

namespace Execview.BasketballAnalytics.Core.UnitTests.Reporting.Enrichers
{
    public sealed class TopAchieversEnricherTests
    {
        private static Player CreatePlayer(string name, decimal ppg)
        {
            return new Player(1, Position.PG, 10, "United States", name, "6 ft 2 in", "175 lb", "Iowa",
                ppg);
        }

        [Fact]
        public void Given_AllPlayers_When_EnrichingReport_Then_ReturnTop3AchieversInTheTeam()
        {
            // Assert
            var sut = new TopAchieversEnricher();
            var builder = Substitute.For<IReportBuilder>();
            var players = new List<Player>
            {
                CreatePlayer("Jordan, Michael", 32.6m),
                CreatePlayer("Pippen, Scottie", 18.6m),
                CreatePlayer("Grant, Horace", 13.2m),
                CreatePlayer("Armstrong, B.J.", 12.3m),
                CreatePlayer("Williams, Scott", 5.9m)
            };

            builder.WithAchievement(Arg.Any<Medal>(), Arg.Any<Player>()).Returns(builder);

            // Act
            sut.EnrichReport(players, builder);

            // Arrange
            Received.InOrder(() =>
            {
                builder.Received(1).WithAchievement(Arg.Is<Medal>(m => m == Medal.Gold),
                    Arg.Is<Player>(p => p.Name.Equals("Jordan, Michael")));

                builder.Received(1).WithAchievement(Arg.Is<Medal>(m => m == Medal.Silver),
                    Arg.Is<Player>(p => p.Name.Equals("Pippen, Scottie")));

                builder.Received(1).WithAchievement(Arg.Is<Medal>(m => m == Medal.Bronze),
                    Arg.Is<Player>(p => p.Name.Equals("Grant, Horace")));
            });
        }
    }
}