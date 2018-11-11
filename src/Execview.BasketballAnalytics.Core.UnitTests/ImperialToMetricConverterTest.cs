using Xunit;

namespace Execview.BasketballAnalytics.Core.UnitTests
{
    public sealed class ImperialToMetricConverterTest
    {
        [Theory]
        [InlineData(6, 10, 208)]
        [InlineData(7, 1, 216)]
        [InlineData(7, 0, 213)]
        [InlineData(6, 2, 188)]
        public void Given_FeetAndInch_When_Converted_Then_ReturnEquivalentValueInCm(int feet, int inches, int expectedValue)
        {
            // Arrange
            var sut = new ImperialToMetricConverter();

            // Act
            var result = sut.ConvertToCm(feet, inches);

            // Assert
            Assert.Equal(expectedValue, result);
        }
    }
}