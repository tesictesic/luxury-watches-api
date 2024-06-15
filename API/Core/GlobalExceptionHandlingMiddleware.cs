using Application.Logging;
using Application;
using FluentValidation;
using Application.Exceptions;

namespace API.Core
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private IExceptionLogger _logger;
        private IApplicationActor _actor;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next, IExceptionLogger logger, IApplicationActor actor)
        {
            _next = next;
            _logger = logger;
            _actor = actor;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception exception)
            {
                if (exception is UnauthorizedAccessException ua)
                {
                    httpContext.Response.StatusCode = 401;
                    var body = new { error = ua.Message };
                    await httpContext.Response.WriteAsJsonAsync(body);

                    return;
                }

                if (exception is ValidationException ex)
                {
                    httpContext.Response.StatusCode = 422;
                    var body = ex.Errors.Select(x => new { Property = x.PropertyName, Error = x.ErrorMessage });

                    await httpContext.Response.WriteAsJsonAsync(body);
                    return;
                }

                if (exception is EntityNotFoundException ek)
                {
                    httpContext.Response.StatusCode = 404;
                    var body = new { error = ek.Message };

                    await httpContext.Response.WriteAsJsonAsync(body);
                    return;
                }

                if (exception is ConflictException c)
                {
                    httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
                    var body = new { error = c.Message };

                    await httpContext.Response.WriteAsJsonAsync(body);
                    return;
                }

                var errorId = _logger.Log(exception);

                httpContext.Response.StatusCode = 500;
                await httpContext.Response.WriteAsJsonAsync(new { Message = $"An unexpected error has occured. Please contact our support with this ID - {errorId}." });
            }
        }
    }
}
