using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeWebAPI.Utils.Exceptions;

namespace TicTacToeWebAPI.Middleware
{
    public class TicTacToeMiddleware
    {
        public static Action<IApplicationBuilder> ExceptionHandler()
        {
            return errorApp =>
            {
                errorApp.Run(async context =>
                {
                    IExceptionHandlerFeature exceptionHandler = context.Features.Get<IExceptionHandlerFeature>();
                    if (exceptionHandler != null)
                    {
                        Exception exception = exceptionHandler.Error;
                        context.Response.StatusCode = exception is HttpResponseException ? ((HttpResponseException)exception).Status : StatusCodes.Status500InternalServerError;
                        context.Response.ContentType = "application/json";
                        byte[] responseBody = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new { msg = exception.Message }));
                        await context.Response.Body.WriteAsync(responseBody, 0, responseBody.Length);
                    }
                });
            };
        }
    }
}
