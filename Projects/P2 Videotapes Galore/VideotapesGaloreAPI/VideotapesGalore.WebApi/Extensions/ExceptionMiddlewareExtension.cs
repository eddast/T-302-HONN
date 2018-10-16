using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using VideotapesGalore.Models.Exceptions;
using VideotapesGalore.Services.Interfaces;

namespace VideotapesGalore.WebApi.Extensions
{
    /// <summary>
    /// Configures a global exception handling on for app
    /// </summary>
    public static class ExceptionMiddlewareExtension
    {
        /// <summary>
        /// Configures a global exception handling on for app
        /// </summary>
        /// <param name="app">app to apply global exception handling on</param>
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(error => 
            {
                // Globally track exceptions to return appropriate http responses and status codes
                error.Run(async context => 
                {
                    // Set up exception handler to listen for exception
                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var exception = exceptionHandlerFeature.Error;

                    // Set default status code for exception is 500 (Internal Server Error) if exception does not match those traced
                    var statusCode = (int) HttpStatusCode.InternalServerError; 

                    // Globally track resource not found exceptions (404) and
                    // Badly formatted input exception (412) when model input is invalid
                    if      (exception is ResourceNotFoundException)    statusCode = (int) HttpStatusCode.NotFound;
                    else if (exception is ParameterFormatException)     statusCode = (int) HttpStatusCode.BadRequest;
                    else if (exception is AuthorizationException)       statusCode = (int) HttpStatusCode.Unauthorized;
                    else if (exception is InputFormatException)         statusCode = (int) HttpStatusCode.PreconditionFailed;

                    // Log explicit exception message when exception occurs to log file
                    var logService = app.ApplicationServices.GetService(typeof(ILogService)) as ILogService;
                    logService.LogToFile($"Exception: {exception.Message}\n\tStatus Code: {statusCode}\n\tStack trace:\n{exception.StackTrace}");

                    // On exception respond with the error model format as a HTTP response back to client
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = statusCode;
                    var exceptionResponse = new ExceptionModel { StatusCode = statusCode, Message = exception.Message };
                    await context.Response.WriteAsync(exceptionResponse.ToString());
                });
            });
        }
    }
}