using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace Kartaca.API.Middleware
{
    public static class LogMiddlewareExtension
    {
        public static IApplicationBuilder UseLogMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LogMiddleware>();
        }
    }
}
