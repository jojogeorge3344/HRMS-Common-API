using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using Chef.Common.Exceptions.Helper;
using Chef.Common.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Chef.Common.Exceptions
{
    public class ErrorHandler
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ErrorHandler> logger;

        public ErrorHandler(
            RequestDelegate next,
            ILogger<ErrorHandler> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context, IHostEnvironment env)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, env);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception, IHostEnvironment env)
        {
            HttpStatusCode status;
            string code;
            string message = null;
            IDictionary data = null;
            string contextMessage = string.Empty;

            Type exceptionType = exception.GetType();
            System.Collections.Generic.IEnumerable<Exception> exceptions = exception.GetInnerExceptions();

            if (exceptions.Any(x => x is SocketException))
            {
                SocketException socketException = (SocketException)exceptions.FirstOrDefault(x => x is SocketException);
                ServiceExceptionCode exceptionCode = SocketExceptionHelper.ErrorCode(socketException);
                status = HttpStatusCode.InternalServerError;
                data = socketException.Data;
                code = exceptionCode.ToString();
                contextMessage = context.GetExceptionMessage(exceptionCode);
                message = !string.IsNullOrEmpty(contextMessage) ? contextMessage : SocketExceptionHelper.ErrorMessage(socketException);
            }
            else if (exceptions.Any(x => x is PostgresException))
            {
                PostgresException dbException = (PostgresException)exceptions.FirstOrDefault(x => x is PostgresException);
                ServiceExceptionCode exceptionCode = PostgresExceptionHelper.ErrorCode(dbException);
                status = HttpStatusCode.InternalServerError;
                data = dbException.Data;
                code = exceptionCode.ToString();
                contextMessage = context.GetExceptionMessage(exceptionCode);
                message = !string.IsNullOrEmpty(contextMessage) ? contextMessage : PostgresExceptionHelper.ErrorMessage(dbException);
            }
            else if (exceptions.Any(x => x is Refit.ApiException))
            {
                Refit.ApiException apiException = (Refit.ApiException)exceptions.FirstOrDefault(x => x is Refit.ApiException);
                ServiceExceptionCode exceptionCode = ApiClientExceptionHelper.ErrorCode(apiException);
                status = apiException.StatusCode;
                data = apiException.Data;
                code = exceptionCode.ToString();
                message = !string.IsNullOrEmpty(apiException.Content) ? apiException.Content : ApiClientExceptionHelper.ErrorMessage(apiException);
            }
            else if (exceptions.Any(x => x is ResourceAlreadyExistsException))
            {
                ResourceAlreadyExistsException ex = (ResourceAlreadyExistsException)exceptions.FirstOrDefault(x => x is ResourceAlreadyExistsException);
                status = HttpStatusCode.Conflict;
                data = ex.Data;
                code = ServiceExceptionCode.DbUniqueKeyViolation.ToString();
                message = ex.Message;
            }
            else if (exceptions.Any(x => x is ResourceHasDependentException))
            {
                ResourceHasDependentException ex = (ResourceHasDependentException)exceptions.FirstOrDefault(x => x is ResourceHasDependentException);
                status = HttpStatusCode.InternalServerError;
                data = ex.Data;
                code = ServiceExceptionCode.DbForeignKeyViolation.ToString();
                message = ex.Message;
            }
            else if (exceptions.Any(x => x is ResourceNotFoundException))
            {
                ResourceNotFoundException ex = (ResourceNotFoundException)exceptions.FirstOrDefault(x => x is ResourceNotFoundException);
                status = HttpStatusCode.NotFound;
                data = ex.Data;
                code = ServiceExceptionCode.ResourceNotFound.ToString();
                message = ex.Message;
            }
            else if (exceptions.Any(x => x is BadRequestException))
            {
                BadRequestException ex = (BadRequestException)exceptions.FirstOrDefault(x => x is BadRequestException);
                status = HttpStatusCode.BadRequest;
                data = ex.Data;
                code = ServiceExceptionCode.BadRequest.ToString();
                message = ex.Message;
            }
            else if (exceptions.Any(x => x is ApplicationException))
            {
                ApplicationException ex = (ApplicationException)exceptions.FirstOrDefault(x => x is ApplicationException);
                status = HttpStatusCode.InternalServerError;
                data = ex.Data;
                code = ServiceExceptionCode.ApplicationException.ToString();
                message = ex.Message;
            }
            else
            {
                status = HttpStatusCode.InternalServerError;
                data = exception.Data;
                code = "Unhandled";
                message = exception.GetMessage();
            }

            logger.LogError(exception.ToString());

            string result = JsonSerializer.Serialize(
                new { code, message, data }
            );

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;

            return context.Response.WriteAsync(result);
        }
    }
}
