﻿using System.Collections.Generic;
using Execview.BasketballAnalytics.Core.Domain;
using Execview.BasketballAnalytics.Core.Reporting.Domain;
using Execview.BasketballAnalytics.Core.Reporting.Enrichers;
using NSubstitute;
using Xunit;

namespace Execview.BasketballAnalytics.Core.UnitTests.Reporting.Enrichers
{
    public sealed class TeamEnricherTests
    {
        private static Player CreatePlayer(decimal ppg)
        {
            return new Player(1, Position.PG, 10, "United States", "Armstrong, B.J.", "6 ft 2 in", "175 lb", "Iowa",
                ppg);
        }

        [Fact]
        public void Given_AllPlayers_When_EnrichingReport_Then_ReturnPlayersSortedByPpg()
        {
            // Assert
            var sut = new TeamEnricher();
            var builder = Substitute.For<IReportBuilder>();
            var players = new List<Player>()
            {
                CreatePlayer(10),
                CreatePlayer(15),
                CreatePlayer(8),
                CreatePlayer(27),
                CreatePlayer(6)
            };

            var result = new List<Player>();
            builder.WithPlayers(Arg.Do<List<Player>>(p => result = p));
      
            // Act
            sut.EnrichReport(players, builder);

            // Arrange
            Assert.Collection(result, p =>
            {
                Assert.Equal(27, p.PointsPerGame);
            }, p =>
            {
                Assert.Equal(15, p.PointsPerGame);
            }, p =>
            {
                Assert.Equal(10, p.PointsPerGame);
            }, p =>
            {
                Assert.Equal(8, p.PointsPerGame);
            }, p =>
            {
                Assert.Equal(6, p.PointsPerGame);
            });
        }
    }
}