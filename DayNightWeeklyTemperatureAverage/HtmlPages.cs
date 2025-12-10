using System.Net;
using System.Text;

namespace DayNightWeeklyTemperatureAverage
{
    internal static class HtmlPages
    {
        private static readonly Dictionary<string, Dictionary<string, string>> Translations = new()
        {
            {
                "en", new()
                {
                    { "title", "Temperature Average" },
                    { "heading", "Day / Night Temperature Averages" },
                    { "description", "Paste forecast lines (e.g. 25°/14°)—one or multiple lines or inline segments. Any occurrence of the pattern number°/number° (also accepts ?) will be included." },
                    { "input_label", "Input:" },
                    { "placeholder", "Mon 25°/14° Tue 27°/16° ..." },
                    { "calculate", "Calculate" },
                    { "clear", "Clear" },
                    { "pairs_found", "Pairs found" },
                    { "day_avg", "Day Avg" },
                    { "night_avg", "Night Avg" },
                    { "result_heading", "Result" },
                    { "averages_computed", "Averages computed:" },
                    { "day_average", "Day average" },
                    { "night_average", "Night average" },
                    { "original_input", "Original Input" },
                    { "back", "Back" },
                    { "language", "Language" }
                }
            },
            {
                "pl", new()
                {
                    { "title", "Œrednia temperatura" },
                    { "heading", "Œrednie temperatury dzienne / nocne" },
                    { "description", "Wklej linie prognozy (np. 25°/14°)—jedn¹ lub wiele linii lub segmentów wbudowanych. Ka¿dy wzorzec liczba°/liczba° (akceptuje równie¿ ?) zostanie uwzglêdniony." },
                    { "input_label", "Wejœcie:" },
                    { "placeholder", "Pn 25°/14° Wt 27°/16° ..." },
                    { "calculate", "Oblicz" },
                    { "clear", "Wyczyœæ" },
                    { "pairs_found", "Znalezione pary" },
                    { "day_avg", "Œrednia dzienne" },
                    { "night_avg", "Œrednia nocne" },
                    { "result_heading", "Wynik" },
                    { "averages_computed", "Obliczone œrednie:" },
                    { "day_average", "Œrednia dzienne" },
                    { "night_average", "Œrednia nocne" },
                    { "original_input", "Oryginalne wejœcie" },
                    { "back", "Wstecz" },
                    { "language", "Jêzyk" }
                }
            },
            {
                "es", new()
                {
                    { "title", "Promedio de temperatura" },
                    { "heading", "Promedios de temperatura diurna / nocturna" },
                    { "description", "Pegue líneas de pronóstico (p. ej. 25°/14°)—una o varias líneas o segmentos en línea. Se incluirá cualquier ocurrencia del patrón número°/número° (también acepta ?)." },
                    { "input_label", "Entrada:" },
                    { "placeholder", "Lun 25°/14° Mar 27°/16° ..." },
                    { "calculate", "Calcular" },
                    { "clear", "Limpiar" },
                    { "pairs_found", "Pares encontrados" },
                    { "day_avg", "Promedio diurno" },
                    { "night_avg", "Promedio nocturno" },
                    { "result_heading", "Resultado" },
                    { "averages_computed", "Promedios calculados:" },
                    { "day_average", "Promedio diurno" },
                    { "night_average", "Promedio nocturno" },
                    { "original_input", "Entrada original" },
                    { "back", "Atrás" },
                    { "language", "Idioma" }
                }
            },
            {
                "de", new()
                {
                    { "title", "Temperaturdurchschnitt" },
                    { "heading", "Durchschnittliche Tages- und Nachttemperaturen" },
                    { "description", "Fügen Sie Prognoszeilen ein (z. B. 25°/14°)—eine oder mehrere Zeilen oder Inline-Segmente. Jedes Vorkommen des Musters Zahl°/Zahl° (akzeptiert auch ?) wird einbezogen." },
                    { "input_label", "Eingabe:" },
                    { "placeholder", "Mo 25°/14° Di 27°/16° ..." },
                    { "calculate", "Berechnen" },
                    { "clear", "Löschen" },
                    { "pairs_found", "Gefundene Paare" },
                    { "day_avg", "Tagesdurchschnitt" },
                    { "night_avg", "Nachtdurchschnitt" },
                    { "result_heading", "Ergebnis" },
                    { "averages_computed", "Berechnete Durchschnitte:" },
                    { "day_average", "Tagesdurchschnitt" },
                    { "night_average", "Nachtdurchschnitt" },
                    { "original_input", "Ursprüngliche Eingabe" },
                    { "back", "Zurück" },
                    { "language", "Sprache" }
                }
            },
            {
                "fr", new()
                {
                    { "title", "Moyenne de température" },
                    { "heading", "Moyennes de température jour / nuit" },
                    { "description", "Collez les lignes de prévision (par ex. 25°/14°)—une ou plusieurs lignes ou segments en ligne. Chaque occurrence du motif nombre°/nombre° (accepte aussi ?) sera incluse." },
                    { "input_label", "Entrée:" },
                    { "placeholder", "Lun 25°/14° Mar 27°/16° ..." },
                    { "calculate", "Calculer" },
                    { "clear", "Effacer" },
                    { "pairs_found", "Paires trouvées" },
                    { "day_avg", "Moy. jour" },
                    { "night_avg", "Moy. nuit" },
                    { "result_heading", "Résultat" },
                    { "averages_computed", "Moyennes calculées:" },
                    { "day_average", "Moyenne jour" },
                    { "night_average", "Moyenne nuit" },
                    { "original_input", "Entrée d'origine" },
                    { "back", "Retour" },
                    { "language", "Langue" }
                }
            }
        };

        public static string BuildFormPage()
        {
            return @"<!DOCTYPE html>
<html lang=""en"">
<head>
<meta charset=""UTF-8"">
<title data-i18n=""title"">Temperature Average</title>
<style>
body { font-family: system-ui, Arial, sans-serif; margin: 2rem; max-width: 860px; }
.lang-toggle { position: absolute; top: 1rem; right: 2rem; display: flex; gap: 0.5rem; align-items: center; }
.lang-toggle span { font-weight: 500; }
.lang-btn { background: none; border: none; cursor: pointer; font-size: 1.5rem; padding: 0.25rem; opacity: 0.6; transition: opacity 0.2s; }
.lang-btn:hover { opacity: 0.8; }
.lang-btn.active { opacity: 1; transform: scale(1.2); }
textarea { width: 100%; height: 160px; font-family: Consolas, monospace; font-size: 0.9rem; }
button { padding: 0.6rem 1.2rem; font-size: 1rem; cursor: pointer; }
#result { margin-top: 1.5rem; padding: 1rem; border: 1px solid #ccc; background: #f9f9f9; white-space: pre; }
code { background:#eee; padding:0.15rem 0.3rem; }
</style>
</head>
<body>
<div class=""lang-toggle"">
    <span data-i18n=""language"">Language</span>:
    <button class=""lang-btn active"" data-lang=""en"" title=""English"">&#127468;&#127463;</button>
    <button class=""lang-btn"" data-lang=""pl"" title=""Polski"">&#127477;&#127473;</button>
    <button class=""lang-btn"" data-lang=""es"" title=""Espa?ol"">&#127466;&#127480;</button>
    <button class=""lang-btn"" data-lang=""de"" title=""Deutsch"">&#127465;&#127466;</button>
    <button class=""lang-btn"" data-lang=""fr"" title=""Français"">&#127467;&#127479;</button>
</div>
<h1 data-i18n=""heading"">Day / Night Temperature Averages</h1>
<p data-i18n=""description"">Paste forecast lines (e.g. <code>25°/14°</code>)—one or multiple lines or inline segments. Any occurrence of the pattern <code>number°/number°</code> (also accepts ?) will be included.</p>
<form id=""tempForm"" method=""post"">
    <label for=""input"" data-i18n=""input_label"">Input:</label><br>
    <textarea id=""input"" name=""input"" data-i18n-placeholder=""placeholder"" placeholder=""Mon 25°/14° Tue 27°/16° ...""></textarea><br><br>
    <button type=""submit"" data-i18n=""calculate"">Calculate</button>
    <button type=""button"" id=""clearBtn"" data-i18n=""clear"">Clear</button>
</form>
<div id=""result"" hidden></div>
<script>
const translations = {
    en: { title: 'Temperature Average', heading: 'Day / Night Temperature Averages', description: 'Paste forecast lines (e.g. 25°/14°)—one or multiple lines or inline segments. Any occurrence of the pattern number°/number° (also accepts ?) will be included.', input_label: 'Input:', placeholder: 'Mon 25°/14° Tue 27°/16° ...', calculate: 'Calculate', clear: 'Clear', pairs_found: 'Pairs found', day_avg: 'Day Avg', night_avg: 'Night Avg', language: 'Language' },
    pl: { title: 'Œrednia temperatura', heading: 'Œrednie temperatury dzienne / nocne', description: 'Wklej linie prognozy (np. 25°/14°)—jedn¹ lub wiele linii lub segmentów wbudowanych. Ka¿dy wzorzec liczba°/liczba° (akceptuje równie¿ ?) zostanie uwzglêdniony.', input_label: 'Wejœcie:', placeholder: 'Pn 25°/14° Wt 27°/16° ...', calculate: 'Oblicz', clear: 'Wyczyœæ', pairs_found: 'Znalezione pary', day_avg: 'Œrednia dzienne', night_avg: 'Œrednia nocne', language: 'Jêzyk' },
    es: { title: 'Promedio de temperatura', heading: 'Promedios de temperatura diurna / nocturna', description: 'Pegue líneas de prognoza (p. ej. 25°/14°)—una o varias líneas o segmentos en línea. Se incluirá cualquier ocurrencia del patrón número°/número° (también acepta ?).', input_label: 'Entrada:', placeholder: 'Lun 25°/14° Mar 27°/16° ...', calculate: 'Calcular', clear: 'Limpiar', pairs_found: 'Pares encontrados', day_avg: 'Promedio diurno', night_avg: 'Promedio nocturno', language: 'Idioma' },
    de: { title: 'Temperaturdurchschnitt', heading: 'Durchschnittliche Tages- und Nachttemperaturen', description: 'Fügen Sie Prognoszeilen ein (z. B. 25°/14°)—eine oder mehrere Zeilen oder Inline-Segmente. Jedes Vorkommen des Musters Zahl°/Zahl° (akzeptiert auch ?) wird einbezogen.', input_label: 'Eingabe:', placeholder: 'Mo 25°/14° Di 27°/16° ...', calculate: 'Berechnen', clear: 'Löschen', pairs_found: 'Gefundene Paare', day_avg: 'Tagesdurchschnitt', night_avg: 'Nachtdurchschnitt', language: 'Sprache' },
    fr: { title: 'Moyenne de température', heading: 'Moyennes de température jour / nuit', description: 'Collez les lignes de prévision (par ex. 25°/14°)—une ou plusieurs lignes ou segments en ligne. Chaque occurrence du motif nombre°/nombre° (accepte aussi ?) sera incluse.', input_label: 'Entrée:', placeholder: 'Lun 25°/14° Mar 27°/16° ...', calculate: 'Calculer', clear: 'Effacer', pairs_found: 'Paires trouvées', day_avg: 'Moy. jour', night_avg: 'Moy. nuit', language: 'Langue' }
};

let currentLang = localStorage.getItem('tempLang') || 'en';

function setLanguage(lang) {
    currentLang = lang;
    localStorage.setItem('tempLang', lang);
    document.documentElement.lang = lang;
    
    document.querySelectorAll('[data-i18n]').forEach(el => {
        const key = el.getAttribute('data-i18n');
        if (translations[lang][key]) {
            el.textContent = translations[lang][key];
        }
    });
    
    document.querySelectorAll('[data-i18n-placeholder]').forEach(el => {
        const key = el.getAttribute('data-i18n-placeholder');
        if (translations[lang][key]) {
            el.placeholder = translations[lang][key];
        }
    });
    
    document.querySelectorAll('.lang-btn').forEach(btn => {
        btn.classList.toggle('active', btn.getAttribute('data-lang') === lang);
    });
    
    document.title = translations[lang]['title'];
}

document.querySelectorAll('.lang-btn').forEach(btn => {
    btn.addEventListener('click', () => setLanguage(btn.getAttribute('data-lang')));
});

setLanguage(currentLang);

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
    const pairsLabel = translations[currentLang]['pairs_found'];
    const dayLabel = translations[currentLang]['day_avg'];
    const nightLabel = translations[currentLang]['night_avg'];
    return `${pairsLabel}: ${data.count}\n${dayLabel}: ${data.dayAvg}\n${nightLabel}: ${data.nightAvg}`;
}
</script>
</body>
</html>";
        }

        public static string BuildResultPage(string input, double dayAvg, double nightAvg)
        {
            int count = TemperatureParser.CountPairs(input);
            var sb = new StringBuilder();
            sb.Append("<!DOCTYPE html><html><head><meta charset=\"UTF-8\"><title data-i18n=\"title\">Result</title><style>");
            sb.Append("body{font-family:system-ui,Arial,sans-serif;margin:2rem;max-width:860px}");
            sb.Append(".lang-toggle{position:absolute;top:1rem;right:2rem;display:flex;gap:0.5rem;align-items:center}");
            sb.Append(".lang-toggle span{font-weight:500}");
            sb.Append(".lang-btn{background:none;border:none;cursor:pointer;font-size:1.5rem;padding:0.25rem;opacity:0.6;transition:opacity 0.2s}");
            sb.Append(".lang-btn:hover{opacity:0.8}");
            sb.Append(".lang-btn.active{opacity:1;transform:scale(1.2)}");
            sb.Append("pre{background:#f4f4f4;padding:1rem;white-space:pre-wrap}");
            sb.Append("a.button{display:inline-block;margin-top:1rem;padding:.6rem 1rem;background:#0366d6;color:#fff;text-decoration:none;border-radius:4px}");
            sb.Append("</style></head><body>");
            sb.Append("<div class=\"lang-toggle\">");
            sb.Append("<span data-i18n=\"language\">Language</span>:");
            sb.Append("<button class=\"lang-btn active\" data-lang=\"en\" title=\"English\">&#127468;&#127463;</button>");
            sb.Append("<button class=\"lang-btn\" data-lang=\"pl\" title=\"Polski\">&#127477;&#127473;</button>");
            sb.Append("<button class=\"lang-btn\" data-lang=\"es\" title=\"Espa?ol\">&#127466;&#127480;</button>");
            sb.Append("<button class=\"lang-btn\" data-lang=\"de\" title=\"Deutsch\">&#127465;&#127466;</button>");
            sb.Append("<button class=\"lang-btn\" data-lang=\"fr\" title=\"Français\">&#127467;&#127479;</button>");
            sb.Append("</div>");
            sb.Append("<h1 data-i18n=\"result_heading\">Result</h1>");
            sb.Append("<p data-i18n=\"averages_computed\">Averages computed:</p>");
            sb.Append("<ul>");
            sb.Append($"<li><span data-i18n=\"pairs_found\">Pairs found</span>: {count}</li>");
            sb.Append($"<li><span data-i18n=\"day_average\">Day average</span>: {dayAvg:F1}</li>");
            sb.Append($"<li><span data-i18n=\"night_average\">Night average</span>: {nightAvg:F1}</li>");
            sb.Append("</ul>");
            sb.Append("<h2 data-i18n=\"original_input\">Original Input</h2><pre>");
            sb.Append(WebUtility.HtmlEncode(input));
            sb.Append("</pre><a class=\"button\" href=\"./TemperatureAverage\" data-i18n=\"back\">Back</a>");
            sb.Append("<script>");
            sb.Append("const translations = {");
            sb.Append("en: { result_heading: 'Result', averages_computed: 'Averages computed:', pairs_found: 'Pairs found', day_average: 'Day average', night_average: 'Night average', original_input: 'Original Input', back: 'Back', language: 'Language' },");
            sb.Append("pl: { result_heading: 'Wynik', averages_computed: 'Obliczone œrednie:', pairs_found: 'Znalezione pary', day_average: 'Œrednia dzienne', night_average: 'Œrednia nocne', original_input: 'Oryginalne wejœcie', back: 'Wstecz', language: 'Jêzyk' },");
            sb.Append("es: { result_heading: 'Resultado', averages_computed: 'Promedios calculados:', pairs_found: 'Pares encontrados', day_average: 'Promedio diurno', night_average: 'Promedio nocturno', original_input: 'Entrada original', back: 'Atrás', language: 'Idioma' },");
            sb.Append("de: { result_heading: 'Ergebnis', averages_computed: 'Berechnete Durchschnitte:', pairs_found: 'Gefundene Paare', day_average: 'Tagesdurchschnitt', night_average: 'Nachtdurchschnitt', original_input: 'Ursprüngliche Eingabe', back: 'Zurück', language: 'Sprache' },");
            sb.Append("fr: { result_heading: 'Résultat', averages_computed: 'Moyennes calculées:', pairs_found: 'Paires trouvées', day_average: 'Moyenne jour', night_average: 'Moyenne nuit', original_input: 'Entrée d\\'origine', back: 'Retour', language: 'Langue' }");
            sb.Append("};");
            sb.Append("let currentLang = localStorage.getItem('tempLang') || 'en';");
            sb.Append("function setLanguage(lang) {");
            sb.Append("currentLang = lang;");
            sb.Append("localStorage.setItem('tempLang', lang);");
            sb.Append("document.documentElement.lang = lang;");
            sb.Append("document.querySelectorAll('[data-i18n]').forEach(el => {");
            sb.Append("const key = el.getAttribute('data-i18n');");
            sb.Append("if (translations[lang][key]) { el.textContent = translations[lang][key]; }");
            sb.Append("});");
            sb.Append("document.querySelectorAll('.lang-btn').forEach(btn => {");
            sb.Append("btn.classList.toggle('active', btn.getAttribute('data-lang') === lang);");
            sb.Append("btn.addEventListener('click', () => setLanguage(btn.getAttribute('data-lang')));");
            sb.Append("});");
            sb.Append("document.title = translations[lang]['result_heading'];");
            sb.Append("}");
            sb.Append("setLanguage(currentLang);");
            sb.Append("</script>");
            sb.Append("</body></html>");
            return sb.ToString();
        }
    }
}