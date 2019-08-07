using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Meiyu.WxService.Extensions
{
    /// <summary>
    /// Frank：这是应该MiddleWare的示例，在项目中暂时未使用。
    /// 参见 https://blog.csdn.net/sd7o95o/article/details/78374718
    /// </summary>
    public static class UseMiddlewareExtensions
    {
        public static IApplicationBuilder UseHelloWorld(this IApplicationBuilder app)
        {
            return app.UseMiddleware<HelloWorldMiddleware>();
        }
    }

    public class HelloWorldMiddleware
    {
        private readonly RequestDelegate _next;

        public HelloWorldMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IGreetingService greetingService)
        {
            var message = greetingService.Greet("World (via DI)");
            await context.Response.WriteAsync(message);
        }
    }

    public interface IGreetingService
    {
        string Greet(string to);
    }

    public class GreetingService : IGreetingService
    {
        public string Greet(string to)
        {
            return $"Hello {to}";
        }
    }

}
