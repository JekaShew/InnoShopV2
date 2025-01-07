using InnoShop.CommonLibrary.Logs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace InnoShop.CommonLibrary.Middleware
{
    public class GlobalResponseHandler(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);

                if (httpContext.Response.StatusCode == StatusCodes.Status429TooManyRequests)
                {
                    var title = "Alert!";
                    var message = "Uff! Too many requests were made!";
                    var statusCode = (int)StatusCodes.Status429TooManyRequests;
                    
                    await ModifyResponse(httpContext, title, message, statusCode);
                }

                if (httpContext.Response.StatusCode == StatusCodes.Status401Unauthorized)
                {
                    var title = "Warning!";
                    var message = "Oh! You are UnAuthorized!";
                    var statusCode = (int)StatusCodes.Status401Unauthorized;
                    
                    await ModifyResponse(httpContext, title, message, statusCode);
                }

                if (httpContext.Response.StatusCode == StatusCodes.Status403Forbidden)
                {
                    var title = "Warning!";
                    var message = "Ou! Sorry! The resource is forbidden for you!";
                    var statusCode = (int)StatusCodes.Status403Forbidden;
                    
                    await ModifyResponse(httpContext, title, message, statusCode);
                }

            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                throw new Exception(ex.Message);
            }
        }

        private static async Task ModifyResponse(
                    HttpContext httpContext, 
                    string title, 
                    string message, 
                    int statusCode)
        {
            httpContext.Response.StatusCode = statusCode;

            await httpContext.Response.WriteAsJsonAsync(new
            {
                Title = title,
                Message = message,
                StatusCode = statusCode
            });
        }
    }
}

