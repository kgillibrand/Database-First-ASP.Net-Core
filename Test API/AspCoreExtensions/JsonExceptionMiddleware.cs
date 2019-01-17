using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CWSTeam.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace CWSTeam.AspCoreExtensions
{
    //Inpired by: https://stackoverflow.com/questions/38630076/asp-net-core-web-api-exception-handling

    /// <summary>
    /// Middleware for delivering a response to unhandled exceptions. Kicks in only when the client is requesting JSON and not something else.
    /// </summary>
    public class JsonUnhandledExceptionMiddleware
    {
        private readonly RequestDelegate nextRequest;

        public JsonUnhandledExceptionMiddleware(RequestDelegate next)
        {
            nextRequest = next;
        }

        public async Task Invoke(HttpContext context, IHostingEnvironment env)
        {
            /*We only do soemthing if the client wants JSON or doesn't specify. Otherwise move on to the next middleware.
              The builtin development exception page is HTML. If running in production then there is an empty response (but a 500 status code).
            */
            string accept = context.Request.Headers["Accept"];
            if (string.IsNullOrWhiteSpace(accept) || accept == "*/*" || accept.Contains(WebserviceCommon.JSON_MIME))
            {
                try
                {
                    await nextRequest(context);
                }
                catch (Exception error)
                {
                    await HandleExceptionAsync(context, env, error);
                }
            }
            else
            {
                await nextRequest.Invoke(context);
            }
        }

        /// <summary>
        /// Write a JSON response for the given exception. Include some additional data if this is a development environment.
        /// </summary>
        private static Task HandleExceptionAsync(HttpContext context, IHostingEnvironment env, Exception error)
        {
            var errors = new string[] { error.Message, error?.InnerException?.Message };
            object data = null;
            if (env.IsDevelopment())
            {
                data = new { error.StackTrace, error.Data };
            }
            var message = new ErrorResponse(errors, data);

            var result = JsonConvert.SerializeObject(message);
            context.Response.ContentType = WebserviceCommon.JSON_MIME;
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(result);
        }
    }
}
