using System;
using Execview.BasketballAnalytics.Core.Data;
using Execview.BasketballAnalytics.Core.Domain;
using Xunit;

namespace Execview.BasketballAnalytics.Core.UnitTests.Data
{
    public class PlayerParserTests
    {
        [Fact]
        public void Given_PlayerDescription_When_AllDataIsPresent_Then_ReturnPlayer()
        {
            // Assert
            var sut = new PlayerParser();
            var data = "1,PG,10,United States,\"Armstrong, B.J.\",6 ft 2 in,175 lb,Iowa,12.3";

            // Act
            var result = sut.Build(data);

            // Arrange
                Assert.Equal(1, result.Id);
                Assert.Equal(Position.PG, result.Position);
                Assert.Equal(10, result.Number);
                Assert.Equal("United States", result.Country);
                Assert.Equal("Armstrong, B.J.", result.Name);
                Assert.Equal("6 ft 2 in", result.Height);
                Assert.Equal("175 lb", result.Weight);
                Assert.Equal("Iowa", result.University);
                Assert.Equal(12.3m, result.PointsPerGame);
        }

        [Theory]
        [InlineData("1, PG, 10, United States,\"Armstrong, B.J.\",6 ft 2 in,175 lb,Iowa")]
        [InlineData("1, PG, 10, United States,\"Armstrong, B.J.\",6 ft 2 in,175 lb,Iowa,12.3, Chicago")]
        public void Given_PlayerDescription_When_ArgumentDoesntMatchExpectedFormat_Then_ReturnThrow(string data)
        {
            // Assert
            var sut = new PlayerParser();

            // Act
            // Arrange
            Assert.Throws<ArgumentException>(() => sut.Build(data));
        }
    }
}
