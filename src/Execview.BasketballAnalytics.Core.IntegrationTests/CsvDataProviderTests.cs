using System;
using System.IO;
using System.Threading.Tasks;
using Execview.BasketballAnalytics.Core.Data;
using Execview.BasketballAnalytics.Core.Domain;
using Xunit;

namespace Execview.BasketballAnalytics.Core.IntegrationTests
{
    public class CsvDataProviderTests
    {
        [Fact]
        public async Task Given_DataFile_When_Loaded_Then_ReturnAllPlayers()
        {
            // Arrange
            var playerParser = new PlayerParser();
            var sut = new CsvDataProvider(playerParser);

            // Act
            var datafile = Path.Combine(AppContext.BaseDirectory, "Data", "chicago-bulls-trimmed.csv");
            var result = await sut.GetPlayersAsync(datafile);

            // Assert
            Assert.Collection(result, p =>
            {
                Assert.Equal(1, p.Id);
                Assert.Equal(Position.PG, p.Position);
                Assert.Equal(10, p.Number);
                Assert.Equal("United States", p.Country);
                Assert.Equal("Armstrong, B.J.", p.Name);
                Assert.Equal("6 ft 2 in", p.Height);
                Assert.Equal("175 lb", p.Weight);
                Assert.Equal("Iowa", p.University);
                Assert.Equal(12.3m, p.PointsPerGame);
            }, p =>
            {
                Assert.Equal(2, p.Id);
                Assert.Equal(Position.C, p.Position);
                Assert.Equal(24, p.Number);
                Assert.Equal("United States", p.Country);
                Assert.Equal("Cartwright, Bill", p.Name);
                Assert.Equal("7 ft 1 in", p.Height);
                Assert.Equal("246 lb", p.Weight);
                Assert.Equal("San Francisco", p.University);
                Assert.Equal(5.6m, p.PointsPerGame);
            }, p =>
            {
                Assert.Equal(3, p.Id);
                Assert.Equal(Position.PF, p.Position);
                Assert.Equal(54, p.Number);
                Assert.Equal("United States", p.Country);
                Assert.Equal("Grant, Horace", p.Name);
                Assert.Equal("6 ft 10 in", p.Height);
                Assert.Equal("245 lb", p.Weight);
                Assert.Equal("Clemson", p.University);
                Assert.Equal(13.2m, p.PointsPerGame);
            });
        }
    }
}