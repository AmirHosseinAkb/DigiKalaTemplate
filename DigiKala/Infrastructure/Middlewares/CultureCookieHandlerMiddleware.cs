using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiKala.Infrastructure.Middlewares
{
    public class CultureCookieHandlerMiddleware
    {
        public CultureCookieHandlerMiddleware(RequestDelegate next)
        {
            Next = next;
        }
        private RequestDelegate Next { get; }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var cultureInfo =
                new System.Globalization.CultureInfo("fa-IR");
            System.Threading.Thread.CurrentThread.CurrentCulture = cultureInfo;
            System.Threading.Thread.CurrentThread.CurrentUICulture = cultureInfo;

            await Next(httpContext);
        }
    }
}
