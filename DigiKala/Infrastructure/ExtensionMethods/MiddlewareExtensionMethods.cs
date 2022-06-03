namespace DigiKala.Infrastructure.ExtensionMethods
{
    public static class MiddlewareExtensionMethods
    {
        public static IApplicationBuilder UseCultureCookie(this IApplicationBuilder app)
        {
            return app.UseMiddleware < DigiKala.Infrastructure.Middlewares.CultureCookieHandlerMiddleware>();
        }

        public static IApplicationBuilder UseCustomStaticFiles(this IApplicationBuilder app)
        {
            return app.UseMiddleware<DigiKala.Infrastructure.Middlewares.CustomStaticFilesHandlerMiddleware>();
        }
    }
}
