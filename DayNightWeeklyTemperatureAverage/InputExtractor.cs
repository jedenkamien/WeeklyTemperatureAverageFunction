using System.Text.Json;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;

namespace DayNightWeeklyTemperatureAverage
{
    internal static class InputExtractor
    {
        public static string ExtractInput(string rawBody, HttpRequestData req)
        {
            if (string.IsNullOrWhiteSpace(rawBody))
                return string.Empty;

            string contentType = req.Headers.TryGetValues("Content-Type", out var vals)
                ? vals.FirstOrDefault() ?? ""
                : "";

            if (contentType.Contains("application/json", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    using var doc = JsonDocument.Parse(rawBody);
                    if (doc.RootElement.TryGetProperty("input", out var prop) && prop.ValueKind == JsonValueKind.String)
                        return prop.GetString() ?? "";
                }
                catch { }
                return rawBody;
            }

            if (contentType.Contains("application/x-www-form-urlencoded", StringComparison.OrdinalIgnoreCase))
            {
                var pairs = rawBody.Split('&', StringSplitOptions.RemoveEmptyEntries);
                foreach (var p in pairs)
                {
                    var kv = p.Split('=', 2);
                    if (kv.Length == 2 && kv[0] == "input")
                        return WebUtility.UrlDecode(kv[1]);
                }
                return string.Empty;
            }

            return rawBody.Trim();
        }
    }
}