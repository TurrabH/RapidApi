using Azure;
using RapidPayService.Controllers;
using RapidPayService.Models;
using System.Net;
using System.Text.Json;

namespace RapidPayService.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<AuthController> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<AuthController> logger)
        {
            this.next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (CustomException ex)
            {
                _logger.LogError(ex.Message);
                await WriteResponse(context, ex: ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                await WriteResponse(context, message: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        private async Task WriteResponse(HttpContext context, CustomException? ex = null, string? message = null, int? statusCode = null)
        {
            CustomException? customException = ex;
            if (customException == null)
            {
                customException = new CustomException
                {
                    ErrorMessage = message ?? "Internal Server Error",
                    StatusCode = statusCode ?? StatusCodes.Status500InternalServerError
                };
                _logger.LogError(customException.Message);

            }
                
            var jsonResponse = JsonSerializer.Serialize(new 
            { 
                StatusCode = customException.StatusCode, 
                Message = customException.ErrorMessage 
            });
            _logger.LogError(jsonResponse);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = customException.StatusCode;
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
