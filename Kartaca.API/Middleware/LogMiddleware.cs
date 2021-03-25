using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kartaca.API.Hubs;
using Kartaca.API.Monitoring.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace Kartaca.API.Middleware
{
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;
        public LogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var timeStamp = DateTime.UtcNow;
            System.Threading.Thread.Sleep(new Random().Next(0, 3) * 1000);

            await _next.Invoke(httpContext);

            var diff = DateTime.UtcNow - timeStamp;
            var request = new RequestObj
            {
                MethodName = httpContext.Request.Method,
                RequestTime = diff.TotalSeconds,
                RequestTimeStamp = timeStamp
            };
            var isHttps = httpContext.Request.IsHttps;
            if (diff.TotalSeconds < 3 && httpContext.Request.Path.Value.Contains("api"))
            {
                var requestHub = httpContext.RequestServices.GetService<IHubContext<RequestHub>>();
                await requestHub.Clients.All.SendAsync("ReceiveRequest", request);
                RequestHub.Requests.Add(request);
                Console.WriteLine("{0},{1},{2}", httpContext.Request.Method, diff.TotalSeconds.ToString(), timeStamp.ToString());
            }

            var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            logger.Info("{0},{1},{2}", httpContext.Request.Method, diff.TotalSeconds.ToString(), timeStamp.ToString());
        }
    }
}
