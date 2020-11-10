using Chef.Common.Types;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chef.Common.Exceptions
{
    public static class Extensions
    {
        public static string GetMessage(this Exception ex)
        {
            if (ex is AggregateException exception) 
                return exception.Flatten().InnerException.Message; 
            return ex.Message;
        }

        public static IEnumerable<Exception> GetInnerExceptions(this Exception ex)
        {
            var innerException = ex;
            do
            {
                if (innerException is AggregateException exception)
                {
                    var innerExceptions = exception.Flatten().InnerExceptions;
                    foreach (var ie in innerExceptions)
                        yield return ie;
                    innerException = exception.InnerException;
                }
                else
                {
                    yield return innerException;
                    innerException = innerException.InnerException;
                }
            }
            while (innerException != null);
        }

        public static string GetAllMessages(this IEnumerable<Exception> exceptions)
        {
            return String.Join(Environment.NewLine, exceptions.Select(x => x.Message).Distinct());
        }

        public static void SetExceptionMessage(this HttpContext httpContext, ServiceExceptionCode serviceExceptionCode, string message)
        {
            if (httpContext.Items.ContainsKey(serviceExceptionCode))
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendLine(httpContext.Items[serviceExceptionCode].ToString());
                builder.AppendLine(message);
                httpContext.Items[serviceExceptionCode] = builder.ToString();
            }
            else
                httpContext.Items.Add(serviceExceptionCode, message);
        }

        public static string GetExceptionMessage(this HttpContext httpContext, ServiceExceptionCode serviceExceptionCode)
        {
            if (httpContext.Items.ContainsKey(serviceExceptionCode))
                return httpContext.Items[serviceExceptionCode].ToString();
            else
                return string.Empty; 
        }
    }
}
