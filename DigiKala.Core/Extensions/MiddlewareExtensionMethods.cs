using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiKala.Core.Extensions
{
    public static class MiddlewareExtensionMethods
    {
        public static IApplicationBuilder UseCultureCookie(this IApplicationBuilder app)
            => app.UseMiddleware<Middlewares.CultureCookieHandlerMiddleware>();
    }
}
