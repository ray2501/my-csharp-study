using Microsoft.AspNetCore.Builder;

namespace homepage
{
    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MarkdownPageExtensions
    {
        public static IApplicationBuilder
            UseMarkdownMiddleware(this IApplicationBuilder builder) => 
            builder.UseMiddleware<MarkdownPage>();
    }
}
