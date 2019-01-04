// //OptionsMiddleware.cs
// // Copyright (c) 2019 01 04All Rights Reserved
// // Bogdan Lyashenko
// // corequatro@gmail.com

using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ZeuxApiServer.Middleware
{
    public class OptionsMiddleware
    {
        private readonly RequestDelegate _next;
        private IHostingEnvironment _environment;

        public OptionsMiddleware(RequestDelegate next, IHostingEnvironment environment)
        {
            _next = next;
            _environment = environment;
        }

        public async Task Invoke(HttpContext context)
        {
            this.BeginInvoke(context);
            await this._next.Invoke(context);
        }
   
        private async void BeginInvoke(HttpContext context)
        {
            if (context.Request.Method == "OPTIONS")
            {
                context.Response.Clear();
                context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                context.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
                context.Response.Headers.Add("Cache-Control", "no-cache");
                context.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST");
                context.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept, Authorization");
                context.Response.Headers.Add("Access-Control-Max-Age", "1728000");
                context.Response.StatusCode = 200;
                await context.Response.WriteAsync("OK");
            }
            else
            {
                context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            }
        }
    }

    public static class OptionsMiddlewareExtensions
    {
        public static IApplicationBuilder UseOptions(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<OptionsMiddleware>();
        }
    }
}
