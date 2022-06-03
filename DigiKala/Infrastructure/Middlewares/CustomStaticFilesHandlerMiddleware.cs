using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiKala.Infrastructure.Middlewares
{
    public class CustomStaticFilesHandlerMiddleware
    {
        public CustomStaticFilesHandlerMiddleware(RequestDelegate next,IHostEnvironment _hostEnvironment)
        {
            Next = next;
            HostEnvironment = _hostEnvironment;  
        }
        private RequestDelegate Next { get; }
        private IHostEnvironment HostEnvironment { get; }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            string requestPath =
                httpContext.Request.Path;
            if (string.IsNullOrWhiteSpace(requestPath))
            {
                await Next(httpContext);
                return;
            }
            if (!requestPath.StartsWith("/"))
            {
                await Next(httpContext);
                return;
            }
            string rootPath = HostEnvironment.ContentRootPath;
            string phyisicalPath = Path.Combine(rootPath, "wwwroot", requestPath);
            if (!File.Exists(phyisicalPath))
            {
                await Next(httpContext);
                return;
            }
            string fileExtension = Path.GetExtension(phyisicalPath).ToLower();

            switch (fileExtension)
            {
                case ".htm":
                case ".html":
                    {
                        httpContext.Response.StatusCode = 200;
                        httpContext.Response.ContentType = "text/html";
                        break;
                    }
                case ".css":
                    {
                        httpContext.Response.StatusCode = 200;
                        httpContext.Response.ContentType = "text/css";
                        break;
                    }
                case ".txt":
                    {
                        httpContext.Response.StatusCode = 200;
                        httpContext.Response.ContentType = "text/plain";
                        break;
                    }
                case ".jpg":
                case ".jpeg":
                    {
                        httpContext.Response.StatusCode = 200;
                        httpContext.Response.ContentType = "image/jpeg";
                        break;
                    }
                default:
                    {
                        await Next(httpContext);
                        return;
                    }
            }
            await httpContext.Response.SendFileAsync(phyisicalPath);
        }
    }
}
