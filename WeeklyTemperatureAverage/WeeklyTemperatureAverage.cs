using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace DayNightWeeklyTemperatureAverage
{
    public class TemperatureFunction
    {
        private static readonly Regex TempPattern = new(@"(\d+)[°º]/(\d+)[°º]", RegexOptions.Compiled);

        [Function("TemperatureAverage")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
        {
            if (HttpMethods.IsGet(req))
            {
                return await BuildHtmlFormResponseAsync(req);
            }

            // POST path
            string rawBody = await new StreamReader(req.Body).ReadToEndAsync();

            string input = ExtractInput(rawBody, req);

            if (string.IsNullOrWhiteSpace(input))
            {
                var bad = req.CreateResponse(HttpStatusCode.BadRequest);
                bad.Headers.Add("Content-Type", "application/json");
                await bad.WriteStringAsync("{\"error\":\"No input provided. Provide plain text, form field 'input', or JSON { \\\"input\\\": \\\"...\\\" }.\"}");
                return bad;
            }

            var (dayAvg, nightAvg) = GetAverageTemperatures(input);

            bool wantsHtml = AcceptsHtml(req);

            if (wantsHtml)
            {
                var htmlResp = req.CreateResponse(HttpStatusCode.OK);
                htmlResp.Headers.Add("Content-Type", "text/html; charset=utf-8");
                await htmlResp.WriteStringAsync(BuildResultHtml(input, dayAvg, nightAvg));
                return htmlResp;
            }
            else
            {
                var json = req.CreateResponse(HttpStatusCode.OK);
                json.Headers.Add("Content-Type", "application/json");
                await json.WriteStringAsync(JsonSerializer.Serialize(new
                {
                    dayAvg = Math.Round(dayAvg, 1),
                    nightAvg = Math.Round(nightAvg, 1),
                    count = CountPairs(input)
                }));
                return json;
            }
        }

        private static async Task<HttpResponseData> BuildHtmlFormResponseAsync(HttpRequestData req)
        {
            var resp = req.CreateResponse(HttpStatusCode.OK);
            resp.Headers.Add("Content-Type", "text/html; charset=utf-8");
            await resp.WriteStringAsync(@"
<!DOCTYPE html>
<html lang=""en"">
<head>
<meta charset=""UTF-8"">
<title>Temperature Average (v1.1)</title>
<style>
body { font-family: system-ui, Arial, sans-serif; margin: 2rem; max-width: 860px; }
textarea { width: 100%; height: 160px; font-family: Consolas, monospace; font-size: 0.9rem; }
button { padding: 0.6rem 1.2rem; font-size: 1rem; cursor: pointer; }
#result { margin-top: 1.5rem; padding: 1rem; border: 1px solid #ccc; background: #f9f9f9; white-space: pre; }
code { background:#eee; padding:0.15rem 0.3rem; }
</style>
</head>
<body>
<h1>Day / Night Temperature Averages (v1.2)</h1>
<p>Paste forecast lines (e.g. <code>25°/14°</code>)—one or multiple lines or inline segments. Any occurrence of the pattern <code>number°/number°</code> (also accepts º) will be included.</p>
<form id=""tempForm"" method=""post"">
    <label for=""input"">Input:</label><br>
    <textarea id=""input"" name=""input"" placeholder=""Mon 25°/14° Tue 27°/16° ...""></textarea><br><br>
    <button type=""submit"">Calculate</button>
    <button type=""button"" id=""clearBtn"">Clear</button>
</form>
<div id=""result"" hidden></div>
<script>
const form = document.getElementById('tempForm');
const resultDiv = document.getElementById('result');
const clearBtn = document.getElementById('clearBtn');

form.addEventListener('submit', async (e) => {
    e.preventDefault();
    const input = document.getElementById('input').value;

    const res = await fetch(window.location.href, {
        method: 'POST',
        headers: { 'Content-Type': 'text/plain' },
        body: input
    });

    if (res.headers.get('Content-Type')?.includes('application/json')) {
        const data = await res.json();
        showResult(formatJsonResult(data, input));
    } else {
        const html = await res.text();
        document.open(); document.write(html); document.close();
    }
});

clearBtn.addEventListener('click', () => {
    document.getElementById('input').value = '';
    resultDiv.hidden = true;
});

function showResult(content) {
    resultDiv.hidden = false;
    resultDiv.textContent = content;
}

function formatJsonResult(data, original) {
    return `Pairs found: ${data.count}
Day Avg: ${data.dayAvg}
Night Avg: ${data.nightAvg}`;
}
</script>
</body>
</html>");
            return resp;
        }

        private static string BuildResultHtml(string input, double dayAvg, double nightAvg)
        {
            int count = CountPairs(input);
            var sb = new StringBuilder();
            sb.Append(@"<!DOCTYPE html><html><head><meta charset=""UTF-8""><title>Result</title><style>
body{font-family:system-ui,Arial,sans-serif;margin:2rem;max-width:860px}
pre{background:#f4f4f4;padding:1rem;white-space:pre-wrap}
a.button{display:inline-block;margin-top:1rem;padding:.6rem 1rem;background:#0366d6;color:#fff;text-decoration:none;border-radius:4px}
</style></head><body>");
            sb.Append("<h1>Result</h1>");
            sb.Append("<p>Averages computed:</p>");
            sb.Append("<ul>");
            sb.Append($"<li>Pairs found: {count}</li>");
            sb.Append($"<li>Day average: {dayAvg:F1}</li>");
            sb.Append($"<li>Night average: {nightAvg:F1}</li>");
            sb.Append("</ul>");
            sb.Append("<h2>Original Input</h2><pre>");
            sb.Append(WebUtility.HtmlEncode(input));
            sb.Append("</pre><a class=\"button\" href=\"./TemperatureAverage\">Back</a></body></html>");
            return sb.ToString();
        }

        private static (double dayAvg, double nightAvg) GetAverageTemperatures(string input)
        {
            var dayTemps = new List<int>();
            var nightTemps = new List<int>();

            foreach (Match match in TempPattern.Matches(input))
            {
                if (int.TryParse(match.Groups[1].Value, out int day) &&
                    int.TryParse(match.Groups[2].Value, out int night))
                {
                    dayTemps.Add(day);
                    nightTemps.Add(night);
                }
            }

            double dayAvg = dayTemps.Count > 0 ? dayTemps.Average() : 0;
            double nightAvg = nightTemps.Count > 0 ? nightTemps.Average() : 0;
            return (dayAvg, nightAvg);
        }

        private static int CountPairs(string input) => TempPattern.Matches(input).Count;

        private static string ExtractInput(string rawBody, HttpRequestData req)
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

        private static bool AcceptsHtml(HttpRequestData req)
        {
            if (req.Headers.TryGetValues("Accept", out var values))
            {
                return values.Any(v => v.Contains("text/html", StringComparison.OrdinalIgnoreCase));
            }
            return false;
        }
    }

    internal static class HttpMethods
    {
        public static bool IsGet(HttpRequestData req) =>
            string.Equals(req.Method, "GET", StringComparison.OrdinalIgnoreCase);
    }
}
