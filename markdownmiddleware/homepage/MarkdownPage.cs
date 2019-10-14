using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace homepage
{
   public class MarkdownPage
    {
        readonly RequestDelegate _next;

        readonly IWebHostEnvironment _env;
        public MarkdownPage(RequestDelegate next, IWebHostEnvironment env)
        {
            _next = next;
            _env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            string ContentRootPath = Directory.GetCurrentDirectory();
            string WebRootPath = Path.Combine(ContentRootPath, "wwwroot");
            var requestPath = context.Request.Path;

            string md = WebRootPath + requestPath;
            string extension = Path.GetExtension(md);
            if (String.Compare(extension, ".md", true)!=0) {
                await _next.Invoke(context);
                return;
            }

            if (!File.Exists(md))
            {
                context.Response.StatusCode = 404;
                context.Response.ContentType = "text/html";
                await context.Response.WriteAsync("ERROR: File Not Found");
                return;
            }

            var mdifle = File.ReadAllText(md);
            var res = CommonMark.CommonMarkConverter.Convert(mdifle);

            context.Response.ContentType = "text/html";
            await context.Response.WriteAsync(res);
        }
    }
}