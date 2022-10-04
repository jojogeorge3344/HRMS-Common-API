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
            return ex is AggregateException exception ? exception.Flatten().InnerException.Message : ex.Message;
        }

        public static IEnumerable<Exception> GetInnerExceptions(this Exception ex)
        {
            Exception innerException = ex;

            do
            {
                if (innerException is AggregateException exception)
                {
                    System.Collections.ObjectModel.ReadOnlyCollection<Exception> innerExceptions = exception.Flatten().InnerExceptions;
                    foreach (Exception ie in innerExceptions)
                    {
                        yield return ie;
                    }

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
            return string.Join(Environment.NewLine, exceptions.Select(x => x.Message).Distinct());
        }


        public static void SetExceptionMessage(this HttpContext httpContext, ServiceExceptionCode serviceExceptionCode, string message)
        {
            if (httpContext.Items.ContainsKey(serviceExceptionCode))
            {
                StringBuilder builder = new();
                _ = builder.AppendLine(httpContext.Items[serviceExceptionCode].ToString());
                _ = builder.AppendLine(message);
                httpContext.Items[serviceExceptionCode] = builder.ToString();
            }
            else
            {
                httpContext.Items.Add(serviceExceptionCode, message);
            }
        }

        public static string GetExceptionMessage(this HttpContext httpContext, ServiceExceptionCode serviceExceptionCode)
        {
            return httpContext.Items.ContainsKey(serviceExceptionCode) ? httpContext.Items[serviceExceptionCode].ToString() : string.Empty;
        }
    }
}
