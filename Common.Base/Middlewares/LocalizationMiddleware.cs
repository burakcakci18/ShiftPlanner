using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Common.Base.Middlewares
{
    public class LocalizationMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly string DefaultLanguage = "null"/*SharedConfigurationLoader.GetCode("DefaultLanguage") ?? "en"*/;
        private static readonly HashSet<string> SupportedLanguages = new(StringComparer.OrdinalIgnoreCase)
        {
           "en", "tr"
        };

        public LocalizationMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var langHeader = context.Request.Headers.TryGetValue("Accept-Language", out var values)
                ? values.ToString()
                : null;

            var culture = GetCulture(langHeader);

            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

            await _requestDelegate(context);
        }

        private CultureInfo GetCulture(string? lang)
        {
            if (string.IsNullOrWhiteSpace(lang) || lang.Length < 2)
                return new CultureInfo(DefaultLanguage);

            var shortLang = lang.Substring(0, 2).ToLowerInvariant();

            return SupportedLanguages.Contains(shortLang)
                ? new CultureInfo(shortLang)
                : new CultureInfo(DefaultLanguage);
        }
    }
}
