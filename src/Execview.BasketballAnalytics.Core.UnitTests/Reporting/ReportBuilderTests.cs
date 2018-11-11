using System;
using System.Collections.Generic;
using Execview.BasketballAnalytics.Core.Domain;
using Execview.BasketballAnalytics.Core.Reporting.Domain;
using Execview.BasketballAnalytics.Core.Reporting.Domain.Achievements;
using Xunit;

namespace Execview.BasketballAnalytics.Core.UnitTests.Reporting
{
    public class ReportBuilderTests
    {
        private static Player CreatePlayer(Position position = Position.PG)
        {
            return new Player(1, position, 10, "United States", "Armstrong, B.J.", "6 ft 2 in", "175 lb", "Iowa",
                12.3m);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Given_ReportBuilder_When_AveragePpgIsInvalid_Then_Throw(decimal height)
        {
            // Assert
            var sut = new ReportBuilder() as ReportBuilder;

            // Act
            // Arrange
            Assert.Throws<ArgumentOutOfRangeException>(() => sut.WithAveragePpg(height));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Given_ReportBuilder_When_AverageHeightIsInvalid_Then_Throw(decimal height)
        {
            // Assert
            var sut = new ReportBuilder() as ReportBuilder;

            // Act
            // Arrange
            Assert.Throws<ArgumentOutOfRangeException>(() => sut.WithAverageHeight(height));
        }

        [Theory]
        [InlineData(Medal.Gold)]
        [InlineData(Medal.Silver)]
        [InlineData(Medal.Bronze)]
        public void Given_ReportBuilder_When_PlayerAndItsAchievementAdded_Then_Return(Medal medal)
        {
            // Assert
            var sut = new ReportBuilder();
            var player = CreatePlayer();

            // Act
            var result = sut.WithAchievement(medal, player) as ReportBuilder;

            // Arrange
            Assert.Collection(result.Leaders, leader =>
            {
                Assert.Equal(medal, leader.Key);
                Assert.Same(player, leader.Value);
            });
        }

        [Fact]
        public void Given_ReportBuilder_When_AverageHeightSet_Then_Return()
        {
            // Assert
            var sut = new ReportBuilder();
            // Act
            var result = sut.WithAverageHeight(200.5m) as ReportBuilder;

            // Arrange
            Assert.Equal(200.5m, result.AverageHeight);
        }

        [Fact]
        public void Given_ReportBuilder_When_AveragePpgSet_Then_Return()
        {
            // Assert
            var sut = new ReportBuilder();
            // Act
            var result = sut.WithAveragePpg(5.8m) as ReportBuilder;

            // Arrange
            Assert.Equal(5.8m, result.AveragePpg);
        }

        [Fact]
        public void Given_ReportBuilder_When_LessThanFivePlayersAssigned_Then_Throw()
        {
            // Assert
            var sut = new ReportBuilder();
            var players = new List<Player>
            {
                CreatePlayer(),
                CreatePlayer(),
                CreatePlayer(),
                CreatePlayer()
            };

            // Act
            // Arrange
            Assert.Throws<ArgumentException>(() => sut.WithPlayers(players));
        }

        [Fact]
        public void Given_ReportBuilder_When_MultiplePlayersAreAddedForSameMedal_Then_Throw()
        {
            // Assert
            var sut = new ReportBuilder();
            var player1 = CreatePlayer();
            var player2 = CreatePlayer();

            // Act
            // Arrange
            Assert.Throws<ArgumentException>(() => sut.WithAchievement(Medal.Gold, player1).WithAchievement(Medal.Gold, player2));
        }

        [Fact]
        public void Given_ReportBuilder_When_NullInsteadOfPlayerPassed_Then_Throw()
        {
            // Assert
            var sut = new ReportBuilder();

            // Act
            // Arrange
            Assert.Throws<ArgumentNullException>(() => sut.WithAchievement(Medal.Gold, null));
        }

        [Fact]
        public void Given_ReportBuilder_When_NullInsteadOfPlayersListIsPassed_Then_Throw()
        {
            // Assert
            var sut = new ReportBuilder();

            // Act
            // Arrange
            Assert.Throws<ArgumentNullException>(() => sut.WithPlayers(null));
        }

        [Fact]
        public void Given_ReportBuilder_When_SamePlayerAddedForMultipleMedals_Then_Throw()
        {
            // Assert
            var sut = new ReportBuilder();
            var player = CreatePlayer();

            // Act
            // Arrange
            Assert.Throws<ArgumentException>(() => sut.WithAchievement(Medal.Gold, player).WithAchievement(Medal.Silver, player));
        }

        [Fact]
        public void Given_ReportBuilder_When_ValidPlayersListAdded_Then_Return()
        {
            // Assert
            var sut = new ReportBuilder();
            var players = new List<Player>
            {
                CreatePlayer(),
                CreatePlayer(),
                CreatePlayer(),
                CreatePlayer(),
                CreatePlayer()
            };

            // Act
            var result = sut.WithPlayers(players) as ReportBuilder;

            // Arrange
            Assert.Equal(players, result.Players);
        }

        [Fact]
        public void Given_ReportBuilder_When_ReportRequested_Then_Return()
        {
            // Assert
            var sut = new ReportBuilder();

            var player1 = CreatePlayer(Position.C);
            var player2 = CreatePlayer(Position.SF);
            var player3 = CreatePlayer(Position.SG);

            var players = new List<Player>
            {
                player1,
                player2,
                player3,
                CreatePlayer(Position.PG),
                CreatePlayer(Position.PG)
            };

            // Act
            var result = sut.WithPlayers(players)
                .WithAveragePpg(10.5m)
                .WithAverageHeight(200.5m)
                .WithAchievement(Medal.Gold, player1)
                .WithAchievement(Medal.Silver, player2)
                .WithAchievement(Medal.Bronze, player3)
                .Build();

            // Arrange
            Assert.Equal(players, result.Players);

            Assert.Collection(result.PlayersPerPosition, p =>
            {
                Assert.Equal(Position.PG, p.Key);
                Assert.Equal(2, p.Value);
            }, p =>
            {
                Assert.Equal(Position.C, p.Key);
                Assert.Equal(1, p.Value);
            }, p =>
            {
                Assert.Equal(Position.PF, p.Key);
                Assert.Equal(0, p.Value);
            }, p =>
            {
                Assert.Equal(Position.SG, p.Key);
                Assert.Equal(1, p.Value);
            }, p =>
            {
                Assert.Equal(Position.SF, p.Key);
                Assert.Equal(1, p.Value);
            });

            Assert.Equal(10.5m, result.AveragePpg);
            Assert.Equal("200.5 cm", result.AverageHeight);

            Assert.Collection(result.Leaders, p =>
            {
                Assert.IsType<GoldMedalAchievement>(p);
                Assert.Equal(12.3m, p.Ppg);
            }, p =>
            {
                Assert.IsType<SilverMedalAchievement>(p);
                Assert.Equal(12.3m, p.Ppg);
            }, p =>
            {
                Assert.IsType<BronzeMedalAchievement>(p);
                Assert.Equal(12.3m, p.Ppg);
            });
        }
    }
}