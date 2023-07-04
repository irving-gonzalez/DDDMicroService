using Microsoft.AspNetCore.Http;
using Serilog;
using System.Text.Json;

namespace DDDMicroservice.Application.MiddleWare
{
    /// <summary>
    /// Middleware to trap and log exceptions.
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger = Log.ForContext<ExceptionMiddleware>();

        private class ErrorResponse
        {
            public string Message { get; set; }
            public string Code { get; set; }
            public Guid Id { get; set; }
            public string RequestId { get; set; }
            public string Type { get; set; }

            public static ErrorResponse FromException(
                Exception exception,
                HttpContext context,
                string code = null,
                string message = null)
            {
                return new ErrorResponse
                {
                    Id = Guid.NewGuid(),
                    Type = exception.GetType().ToString(),
                    RequestId = context.TraceIdentifier,
                    Code = code,
                    Message = message,
                };
            }
        }

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        private ErrorResponse GetErrorResponse(Exception exception, HttpContext context)
        {
            switch (exception)
            {
                default:
                    return ErrorResponse.FromException(exception, context, ErrorCode.General, exception.Message);
            }
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                var errorReponse = GetErrorResponse(exception, context);

                _logger.Error(exception, "RequestId: {requestId}, Id: {id}", errorReponse.RequestId, errorReponse.Id);

                var result = JsonSerializer.Serialize(new { error = errorReponse });
                await response.WriteAsync(result);
            }
        }
    }
}
