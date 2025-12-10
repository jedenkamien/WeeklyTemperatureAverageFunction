using System;
using Xunit;
using DayNightWeeklyTemperatureAverage;

namespace DayNightWeeklyTemperatureAverage.Tests
{
    public class TemperatureFunctionTests
    {
        [Theory]
        [InlineData("Mon 25°/14° Tue 27°/16°", 26.0, 15.0)]
        [InlineData("20°/10° 22°/12° 24°/14°", 22.0, 12.0)]
        [InlineData("", 0.0, 0.0)]
        [InlineData("No temps here", 0.0, 0.0)]
        [InlineData("30°/20°", 30.0, 20.0)]
        public void GetAverageTemperatures_ReturnsExpectedAverages(string input, double expectedDay, double expectedNight)
        {
            var (dayAvg, nightAvg) = TemperatureFunctionTestProxy.GetAverageTemperatures(input);
            Assert.Equal(expectedDay, dayAvg, 1);
            Assert.Equal(expectedNight, nightAvg, 1);
        }
    }

    // Proxy to access internal static method for testing
    internal static class TemperatureFunctionTestProxy
    {
        public static (double, double) GetAverageTemperatures(string input)
        {
            var method = typeof(TemperatureFunction).GetMethod("GetAverageTemperatures", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            return ((double day, double night))method.Invoke(null, new object[] { input });
        }
    }
}
