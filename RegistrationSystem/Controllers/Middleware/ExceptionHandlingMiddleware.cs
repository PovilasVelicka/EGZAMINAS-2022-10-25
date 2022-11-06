using Microsoft.EntityFrameworkCore;
using RegistrationSystem.Controllers.DTOs;
using System.Net;

namespace RegistrationSystem.Controllers.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware (
            RequestDelegate requestDelegate,
            ILogger<ExceptionHandlingMiddleware> logger)
        {
            _requestDelegate = requestDelegate;
            _logger = logger;
        }

        public async Task InvokeAsync (HttpContext context)
        {
            try
            {
                await _requestDelegate(context);

            }
            catch (KeyNotFoundException ex)
            {
                await HandleExceptionAsync(
                    context,
                    ex.Message,
                    HttpStatusCode.NotFound,
                    "Key not found");
            }
            catch (InvalidOperationException ex)
            {
                await HandleExceptionAsync(
                    context,
                    ex.Message,
                    HttpStatusCode.BadRequest,
                    "Invalid Operation Exception");
            }
            catch (DbUpdateException ex)
            {
                await HandleExceptionAsync(
                  context,
                  $"{ex.Message}\n{ex.InnerException?.Message}",
                  HttpStatusCode.BadRequest,
                  "An error occurred while saving the changes");
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(
                    context,
                    $"{ex.Message}\n{ex.InnerException?.Message}",
                    HttpStatusCode.InternalServerError,
                    "Internal server error");
            }
        }

        private async Task HandleExceptionAsync (
            HttpContext context,
            string exMsg,
            HttpStatusCode statusCode,
            string message)
        {
            _logger.LogError(message: exMsg);

            var response = context.Response;
            response.StatusCode = (int)statusCode;

            var errorDto = new ErrorResponse
            {
                StatusCode = (int)statusCode,
                Message = message,
            };

            await response.WriteAsJsonAsync(errorDto);
        }
    }
}
