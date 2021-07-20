using Microsoft.AspNetCore.Builder;

namespace MeetHut.Backend.Middlewares
{
    /// <summary>
    /// Exception handler extension
    /// </summary>
    public static class ExceptionHandlerMiddlewareExtensions
    {
        /// <summary>
        /// Register exception handler
        /// </summary>
        /// <param name="app">Application</param>
        public static void UseServerExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}