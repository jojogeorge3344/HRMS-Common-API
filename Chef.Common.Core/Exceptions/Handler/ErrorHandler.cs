using Chef.Common.Exceptions.Helper;
using Chef.Common.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Npgsql;
using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;

namespace Chef.Common.Exceptions
{
    public class ErrorHandler
    {
        private readonly RequestDelegate next;

        public ErrorHandler(RequestDelegate next)
        {
            this.next = next;
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

        private static Task HandleExceptionAsync(HttpContext context, Exception exception, IHostEnvironment env)
        {
            HttpStatusCode status;
            string code;
            string message = null;
            object detailMessage = null;
            var stackTrace = String.Empty;
            IDictionary data = null;
            var exceptionType = exception.GetType();
            var exceptions = exception.GetInnerExceptions();
            //if (exceptions.Any(x => x is ServiceException))
            //{
            //    var serviceException = (ServiceException)exceptions.FirstOrDefault(x => x is ServiceException);
            //    status = HttpStatusCode.InternalServerError;
            //    data = serviceException.Data;
            //    code = serviceException.ExceptionCode.ToString();
            //    var contextMessage = context.GetExceptionMessage(serviceException.ExceptionCode);
            //    message = !string.IsNullOrEmpty(contextMessage) ? contextMessage : serviceException.Message;
            //}
            //else 
            if (exceptions.Any(x => x is SocketException))
            {
                var socketException = (SocketException)exceptions.FirstOrDefault(x => x is SocketException);
                var exceptionCode = SocketExceptionHelper.ErrorCode(socketException);
                status = HttpStatusCode.InternalServerError;
                data = socketException.Data;
                code = exceptionCode.ToString();
                var contextMessage = context.GetExceptionMessage(exceptionCode);
                message = !string.IsNullOrEmpty(contextMessage) ? contextMessage : SocketExceptionHelper.ErrorMessage(socketException);
            }
            else if (exceptions.Any(x => x is PostgresException))
            {
                var dbException = (PostgresException)exceptions.FirstOrDefault(x => x is PostgresException);
                var exceptionCode = PostgresExceptionHelper.ErrorCode(dbException);
                status = HttpStatusCode.InternalServerError;
                data = dbException.Data;
                code = exceptionCode.ToString();
                var contextMessage = context.GetExceptionMessage(exceptionCode);
                message = !string.IsNullOrEmpty(contextMessage) ? contextMessage : PostgresExceptionHelper.ErrorMessage(dbException);
            }
            else if (exceptions.Any(x => x is Refit.ApiException))
            {
                var apiException = (Refit.ApiException)exceptions.FirstOrDefault(x => x is Refit.ApiException);
                var exceptionCode = ApiClientExceptionHelper.ErrorCode(apiException);
                status = apiException.StatusCode;
                data = apiException.Data;
                code = exceptionCode.ToString();
                message = !string.IsNullOrEmpty(apiException.Content) ? apiException.Content : ApiClientExceptionHelper.ErrorMessage(apiException);
                detailMessage = ApiClientExceptionHelper.DetailMessage(apiException);
            }
            else if (exceptions.Any(x => x is ResourceAlreadyExistsException))
            {
                var ex = (ResourceAlreadyExistsException)exceptions.FirstOrDefault(x => x is ResourceAlreadyExistsException);
                status = HttpStatusCode.Conflict;
                data = ex.Data;
                code = ServiceExceptionCode.DbUniqueKeyViolation.ToString();
                message = ex.Message;
            }
            else if (exceptions.Any(x => x is ResourceHasDependentException))
            {
                var ex = (ResourceHasDependentException)exceptions.FirstOrDefault(x => x is ResourceHasDependentException);
                status = HttpStatusCode.InternalServerError;
                data = ex.Data;
                code = ServiceExceptionCode.DbForeignKeyViolation.ToString();
                message = ex.Message;
            }
            else if (exceptions.Any(x => x is ResourceNotFoundException))
            {
                var ex = (ResourceNotFoundException)exceptions.FirstOrDefault(x => x is ResourceNotFoundException);
                status = HttpStatusCode.NotFound;
                data = ex.Data;
                code = ServiceExceptionCode.ResourceNotFound.ToString();
                message = ex.Message; 
            }
            else if (exceptions.Any(x => x is BadRequestException))
            {
                var ex = (BadRequestException)exceptions.FirstOrDefault(x => x is BadRequestException);
                status = HttpStatusCode.BadRequest;
                data = ex.Data;
                code = ServiceExceptionCode.BadRequest.ToString();
                message = ex.Message;
            }          
            else
            {
                status = HttpStatusCode.InternalServerError;
                data = exception.Data;
                code = "Unhandled";
                message = exception.GetMessage();
            }

            if (detailMessage == null)
                detailMessage = exceptions.GetAllMessages();
            stackTrace = exception.StackTrace;
            var errorMessage = JsonSerializer.Serialize(
              new { code, message, data, detailMessage, stackTrace }
          );
            var path = @"C:\ErrorLogs";
            bool folderExists = Directory.Exists(path);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            File.AppendAllText(@"C:/ErrorLogs/chef.finance_" + String.Format("{0:dd/MM/yyyy}", DateTime.UtcNow) + ".txt", Environment.NewLine + DateTime.UtcNow.ToString("dd MMMM yyyy HH:mm:ss") + Environment.NewLine + errorMessage + Environment.NewLine);

            if (!env.IsEnvironment("Development"))
            {
                data = null;
                detailMessage = null;
                stackTrace = null;
            }
            var result = JsonSerializer.Serialize(
                new { code, message, data, detailMessage, stackTrace }
            );
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;
            return context.Response.WriteAsync(result);
        }
    }
}
