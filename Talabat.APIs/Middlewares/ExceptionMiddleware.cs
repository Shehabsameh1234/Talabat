using System.Net;
using System.Text.Json;
using Talabat.APIs.Errors;

namespace Talabat.APIs.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware> logger,IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }
        public async Task InvokeAsync(HttpContext httpContext )
        {
            try
            {
                await _next.Invoke(httpContext);
            }
            catch (Exception ex)
            {
                //log the exception
                _logger.LogError(ex.Message);

                //set the status code and type
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType= "application/json";

                //set the response type
                var response = _env.IsDevelopment() ? new ServerErrorException
                    ((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString()) :
                    new ServerErrorException((int)HttpStatusCode.InternalServerError);

                //convert response to json
                var josn =JsonSerializer.Serialize(response).ToLowerInvariant();

                await httpContext.Response.WriteAsync(josn);
                
            }
        }
    }
}
