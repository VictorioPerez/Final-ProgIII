using FinalProg.Middleware.Exceptions;
using System.Net;
using System.Text.Json;

namespace FinalProg.Middleware
{
    public class ErrorMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case ExceptionBadRequestClient e:
                        // 400 Bad Request
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException e:
                        // 404 Not Found
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case UnauthorizedAccessException:
                        // 401 Unautorized
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        break;
                    default:
                        // 500 Unhandle Errors
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                var messageError = error?.Message;
                if (error?.InnerException != null)
                    messageError += error.InnerException;
                var result = JsonSerializer.Serialize(new { message = messageError });
                await response.WriteAsync(result);
            }
        }
    }
}
