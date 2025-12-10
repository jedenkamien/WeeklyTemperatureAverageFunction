using Xunit;

namespace DayNightWeeklyTemperatureAverage.Tests
{
    public class TemperatureFunctionTests
    {
        // teraz dodać wsparcie dla PL oraz dodać test z ujemnymi temperaturami i poprosić o poprawienie kodu;
        // potem można robić deployment

        [Theory]
        [InlineData("Mon 25°/14° Tue 27°/16°", 26.0, 15.0)]
        [InlineData("20°/10° 22°/12° 24°/14°", 22.0, 12.0)]
        [InlineData("", 0.0, 0.0)]
        [InlineData("No temps here", 0.0, 0.0)]
        [InlineData("30°/20°", 30.0, 20.0)]
        public void GetAverageTemperatures_ReturnsExpectedAverages(string input, double expectedDay, double expectedNight)
        {
            var (dayAvg, nightAvg) = TemperatureStatistics.GetAverageTemperatures(input);
            Assert.Equal(expectedDay, dayAvg, 1);
            Assert.Equal(expectedNight, nightAvg, 1);
        }

        [Fact]
        public void ComplexMultilineInput_OnlyPositiveValues_ComputesExpectedValues()
        {
            var input = @"pon.

15°/10°

wt.

16°/7°

śr.

15°/7°

czw.

14°/7°

pt.

15°/7°

sob.

15°/7°

niedz.

16°/8°";

            var (dayAvg, nightAvg) = TemperatureStatistics.GetAverageTemperatures(input);
            Assert.Equal(7, TemperatureParser.CountPairs(input));
            Assert.Equal(15.1, dayAvg, 1); // 106/7 = 15.142857 -> 15.1 (one decimal)
            Assert.Equal(7.6, nightAvg, 1); // 53/7 = 7.571428 -> 7.6 (one decimal)
        }
    }
}