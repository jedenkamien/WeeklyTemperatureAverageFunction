using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;
using System.Text.Json;

namespace DayNightWeeklyTemperatureAverage
{
    // Orchestrator Azure Function – delegates work to extracted classes.
    public class TemperatureFunction
    {
        [Function("TemperatureAverage")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
        {
            if (HttpMethods.IsGet(req))
            {
                // Return HTML form page
                var resp = req.CreateResponse(HttpStatusCode.OK);
                resp.Headers.Add("Content-Type", "text/html; charset=utf-8");
                await resp.WriteStringAsync(HtmlPages.BuildFormPage());
                return resp;
            }

            // POST path
            string rawBody = await new StreamReader(req.Body).ReadToEndAsync();
            string input = InputExtractor.ExtractInput(rawBody, req);

            if (string.IsNullOrWhiteSpace(input))
            {
                var bad = req.CreateResponse(HttpStatusCode.BadRequest);
                bad.Headers.Add("Content-Type", "application/json");
                await bad.WriteStringAsync("{\"error\":\"No input provided. Provide plain text, form field 'input', or JSON { \\\"input\\\": \\\"...\\\" }.\"}");
                return bad;
            }

            var (dayAvg, nightAvg) = TemperatureStatistics.GetAverageTemperatures(input);

            bool wantsHtml = ContentNegotiation.AcceptsHtml(req);
            if (wantsHtml)
            {
                var htmlResp = req.CreateResponse(HttpStatusCode.OK);
                htmlResp.Headers.Add("Content-Type", "text/html; charset=utf-8");
                await htmlResp.WriteStringAsync(HtmlPages.BuildResultPage(input, dayAvg, nightAvg));
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
                    count = TemperatureParser.CountPairs(input)
                }));
                return json;
            }
        }
    }

    internal static class HttpMethods
    {
        public static bool IsGet(HttpRequestData req) =>
            string.Equals(req.Method, "GET", StringComparison.OrdinalIgnoreCase);
    }
}