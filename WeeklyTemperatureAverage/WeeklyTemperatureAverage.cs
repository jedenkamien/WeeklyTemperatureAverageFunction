using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Text.RegularExpressions;

namespace DayNightWeeklyTemperatureAverage
{
    public class WeeklyTemperatureAverage
    {
        [Function("TemperatureAverage")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            var input = await new StreamReader(req.Body).ReadToEndAsync();
            var (dayAvg, nightAvg) = GetAverageTemperatures(input);

            var response = req.CreateResponse();
            response.Headers.Add("Content-Type", "application/json");
            await response.WriteStringAsync($"{{\"dayAvg\":{dayAvg:F1},\"nightAvg\":{nightAvg:F1}}}");
            return response;
        }

        private static (double dayAvg, double nightAvg) GetAverageTemperatures(string input)
        {
            var dayTemps = new List<int>();
            var nightTemps = new List<int>();
            var regex = new Regex(@"(\d+)°/(\d+)°");
            foreach (Match match in regex.Matches(input))
            {
                int day = int.Parse(match.Groups[1].Value);
                int night = int.Parse(match.Groups[2].Value);
                dayTemps.Add(day);
                nightTemps.Add(night);
            }
            double dayAvg = dayTemps.Count > 0 ? dayTemps.Average() : 0;
            double nightAvg = nightTemps.Count > 0 ? nightTemps.Average() : 0;
            return (dayAvg, nightAvg);
        }
    }
}

