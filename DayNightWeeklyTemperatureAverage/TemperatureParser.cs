using System.Text.RegularExpressions;

namespace DayNightWeeklyTemperatureAverage
{
    internal static class TemperatureParser
    {
        private static readonly Regex TempPattern = new("(-?\\d+)[°?]/(-?\\d+)[°?]", RegexOptions.Compiled);

        public static IEnumerable<(int day, int night)> ParsePairs(string input)
        {
            foreach (Match match in TempPattern.Matches(input))
            {
                if (int.TryParse(match.Groups[1].Value, out int day) &&
                    int.TryParse(match.Groups[2].Value, out int night))
                {
                    yield return (day, night);
                }
            }
        }

        public static int CountPairs(string input) => TempPattern.Matches(input).Count;
    }
}