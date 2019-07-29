using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Questionnaire.Extensions
{
    public static class BuilderExtensions
    {
        public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder app, ILogger logger)
        {
            return app.UseMiddleware<CustomExceptionMiddleware>(logger);
        }
    }
}
