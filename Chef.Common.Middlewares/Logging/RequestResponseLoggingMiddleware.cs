using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Common.Middlewares.Logging
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;
        public RequestResponseLoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            this._next = next;
            this._logger = loggerFactory.CreateLogger<RequestResponseLoggingMiddleware>();
            this._recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }
        public async Task Invoke(HttpContext context)
        {
            await LogRequest(context);
           await LogResponse(context);
        }
        private async Task LogRequest(HttpContext context)
        {
            context.Request.EnableBuffering();
            await using var requestStream = this._recyclableMemoryStreamManager.GetStream();
            await context.Request.Body.CopyToAsync(requestStream);
            this._logger.LogInformation(new EventId(1002, "ClientLogger"),"Http Request => Scheme : {Scheme}, Host : {Host}, Method : {Method}, Path : {Path}, QueryString : {QueryString}, PayLoad : {Payload}", 
                context.Request.Scheme, context.Request.Host, context.Request.Method, context.Request.Path, context.Request.QueryString,
                ReadStreamInChunks(requestStream));
            context.Request.Body.Position = 0;
            

        }
        private async Task LogResponse(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;
            await using var responseBody = _recyclableMemoryStreamManager.GetStream();
            context.Response.Body = responseBody;
            await _next(context);
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            this._logger.LogInformation(new EventId(1003, "ClientLogger"), "Http Response => Scheme : {Scheme}, Host : {Host}, Method : {Method}, Path : {Path}, QueryString : {QueryString}, Status: {Status}, PayLoad : {Payload}",
                 context.Request.Scheme, context.Request.Host, context.Request.Method, context.Request.Path, context.Request.QueryString, context.Response.StatusCode,
                 text);
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            await responseBody.CopyToAsync(originalBodyStream);
        }
        private static string ReadStreamInChunks(Stream strem)
        {
            const int chunkSize = 4096;
            strem.Seek(0, SeekOrigin.Begin);
            using var textWriter = new StringWriter();
            using var reader = new StreamReader(strem);
            var readChunk = new char[chunkSize];
            int readChunkLength = 0;
            do
            {
                readChunkLength = reader.ReadBlock(readChunk,0, chunkSize);
                textWriter.Write(readChunk, 0, readChunkLength);

            }while (readChunkLength > 0);
            return textWriter.ToString();
        }
    }
}
