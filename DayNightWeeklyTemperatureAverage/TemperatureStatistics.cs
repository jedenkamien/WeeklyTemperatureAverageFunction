namespace DayNightWeeklyTemperatureAverage
{
    public static class TemperatureStatistics
    {
        public static (double dayAvg, double nightAvg) GetAverageTemperatures(string input)
        {
            var dayTemps = new List<int>();
            var nightTemps = new List<int>();

            foreach (var (day, night) in TemperatureParser.ParsePairs(input))
            {
                dayTemps.Add(day);
                nightTemps.Add(night);
            }

            double dayAvg = dayTemps.Count > 0 ? dayTemps.Average() : 0;
            double nightAvg = nightTemps.Count > 0 ? nightTemps.Average() : 0;
            return (dayAvg, nightAvg);
        }
    }
}