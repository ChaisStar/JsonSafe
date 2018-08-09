namespace JsonSafe.WebApi.Infrastructure.Middlewares
{
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    public class RedirectToStaticFile
    {
        private readonly RequestDelegate _next;
        private readonly string _filename;
        private readonly string _apiPrefix;

        public RedirectToStaticFile(RequestDelegate next, string filename, string apiPrefix)
        {
            _next = next;
            _filename = filename;
            _apiPrefix = apiPrefix;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value;

            if (!path.StartsWith($"/{_apiPrefix}", System.StringComparison.Ordinal) && !Path.HasExtension(path))
                context.Request.Path = _filename;

            await (_next?.Invoke(context) ?? Task.CompletedTask);
        }
    }
}
