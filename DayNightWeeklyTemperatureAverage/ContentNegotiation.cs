using Microsoft.Azure.Functions.Worker.Http;
using System.Linq;

namespace DayNightWeeklyTemperatureAverage
{
    internal static class ContentNegotiation
    {
        public static bool AcceptsHtml(HttpRequestData req)
        {
            if (req.Headers.TryGetValues("Accept", out var values))
            {
                return values.Any(v => v.Contains("text/html", StringComparison.OrdinalIgnoreCase));
            }
            return false;
        }
    }
}