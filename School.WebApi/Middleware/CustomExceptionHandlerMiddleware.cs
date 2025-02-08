using FluentValidation;
using School.Application.Common.Exceptions;
using System.Net;
using System.Text.Json;

namespace School.WebApi.Middleware
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = string.Empty;

            switch (exception)
            {
                case ValidationException validationException:
                    code = HttpStatusCode.BadRequest;  // 400
                    result = JsonSerializer.Serialize(validationException.Errors);
                    break;
                case NotContainsException:
                    code = HttpStatusCode.BadRequest;  // 400
                    break;
                case NoAccessException:
                    code = HttpStatusCode.Forbidden;   // 403
                    break;
                case NotFoundException:
                    code = HttpStatusCode.NotFound;    // 404
                    break;
                case ActionAlreadyCompletedException:
                    code = HttpStatusCode.MethodNotAllowed;  // 405
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            if (result == string.Empty)
            {
                result = JsonSerializer.Serialize(new { error = exception.Message });
            }

            return context.Response.WriteAsync(result);
        }
    }
}
