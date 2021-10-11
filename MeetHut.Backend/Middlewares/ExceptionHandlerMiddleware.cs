using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MeetHut.Backend.Middlewares
{
    /// <summary>
    /// Global Exception handler
    /// </summary>
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        
        /// <summary>
        /// Init Middleware
        /// </summary>
        /// <param name="requestDelegate">Request</param>
        public ExceptionHandlerMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        /// <summary>
        /// Invoke request
        /// </summary>
        /// <param name="httpContext">HTTP Context</param>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _requestDelegate.Invoke(httpContext);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                await HandleExceptionAsync(httpContext, e);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

            await httpContext.Response.WriteAsync(new ServerException
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = exception.Message
            }.ToString());
        }
    }
}