﻿using Microsoft.AspNetCore.Http;

namespace InnoShop.CommonLibrary.Middleware
{
    public class ListenToOnlyApiGatewayRule(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            var signedHeader = context.Request.Headers["ApiGateway"];

            if (signedHeader.FirstOrDefault() is null)
            {
                context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                await context.Response.WriteAsync("Sorry! Service is unavailable!");
                return;
            }
            else
            {
                await next(context);
            }
        }
    }
}
