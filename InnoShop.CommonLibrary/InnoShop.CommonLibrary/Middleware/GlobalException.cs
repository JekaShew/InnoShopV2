using InnoShop.CommonLibrary.Logs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace InnoShop.CommonLibrary.Middleware
{
    public class GlobalException(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            string message = "Oops! Something went wrong! Internal server error occured! Please, try again! ";
            int statusCode = (int)HttpStatusCode.InternalServerError;
            string title = "Error!";

            try
            {
                await next(context);

                if(context.Response.StatusCode == StatusCodes.Status429TooManyRequests)
                {
                    title = "Warning!";
                    message = "Uff! Too many requests were made!";
                    statusCode = (int)StatusCodes.Status429TooManyRequests;
                    await ModifyHeaders(context, title, message, statusCode);
                }

                if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
                {
                    title = "Alert!";
                    message = "Oh! You are unauthorized!";
                    statusCode = (int)StatusCodes.Status401Unauthorized;
                    await ModifyHeaders(context, title, message, statusCode);
                }

                if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
                {
                    title = "Forbidden resource!";
                    message = "Ou! Sorry! The resource is forbidden for you!";
                    statusCode = (int)StatusCodes.Status403Forbidden;
                    await ModifyHeaders(context, title, message, statusCode);
                }

                if (context.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    title = "Not Found!";
                    message = "Ou! Sorry! The resource is not found!";
                    statusCode = (int)StatusCodes.Status404NotFound;
                    await ModifyHeaders(context, title, message, statusCode);
                }
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                if(ex is TaskCanceledException || ex is TimeoutException)
                {
                    message = "Out of time!";
                    title = "Request timeout! Please, try again!";
                    statusCode = (int)StatusCodes.Status408RequestTimeout;
                    await ModifyHeaders(context,title,message,statusCode);
                }

                await ModifyHeaders(context, title, message, statusCode);
            }

            
        }
        private static async Task ModifyHeaders(HttpContext context, string title, string message, int statusCode)
        {
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(new ProblemDetails()
            {
                Detail = message,
                Status = statusCode,
                Title = title,
            }), CancellationToken.None);
            return;
        }

    }
}
