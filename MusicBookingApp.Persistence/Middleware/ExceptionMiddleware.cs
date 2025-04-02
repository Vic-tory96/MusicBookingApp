using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MusicBookingApp.Persistence.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext); // pass the request to the next middleware
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            // Custom handling based on exception type
            switch (exception)
            {
                case NotFoundException _:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound; // 404
                    break;

                case UnauthorizedAccessException _:
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized; // 401
                    break;

                case BadRequestException _:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest; // 400
                    break;

                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; // 500 for unknown errors
                    break;
            }

            var response = new
            {
                success = false,
                statusCode = context.Response.StatusCode,
                message = exception.Message,
                exceptionMessage = exception.Message // Optionally include the exception message for debugging
            };

            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
