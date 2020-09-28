using Chef.Common.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Chef.Common.Exceptions.Helper
{
    public class ApiClientExceptionHelper
    {
        public static ServiceExceptionCode ErrorCode(Refit.ApiException ae)
        {
            return ae.StatusCode switch
            {
                HttpStatusCode.RequestTimeout => ServiceExceptionCode.ApiClientRequestTimeout,
                HttpStatusCode.Unauthorized => ServiceExceptionCode.ApiClientUnauthorized,
                HttpStatusCode.NotFound => ServiceExceptionCode.ApiClientResourceNotFound, 
                _ => ServiceExceptionCode.ApiClientInternalServerError

            };
        }
        public static string ErrorMessage(Refit.ApiException ae)
        {
            return ae.StatusCode switch
            { 
                HttpStatusCode.RequestTimeout => "Apiclient operation timed out.",
                HttpStatusCode.Unauthorized => "Access to the apiclient resoruce is denied.",
                HttpStatusCode.NotFound => "Apiclient resoruce not found.",
                HttpStatusCode.ServiceUnavailable => "Apiclient service is Unavailable.",
                HttpStatusCode.InternalServerError => GetInternalServerErrorMessage(ae),
                _ => ae.ReasonPhrase

            };
        }

        public static string Content(Refit.ApiException ae)
        {
            return ae.Content;
        }

        public static string DetailMessage(Refit.ApiException ae)
        {
            if (ae.Content != null && IsValidJson(ae.Content))
            {
                var content = JsonConvert.DeserializeObject<dynamic>(ae.Content);
                try
                {
                    return Convert.ToString(content.error.detailMessage);
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }


        static string GetInternalServerErrorMessage(Refit.ApiException ae)
        {
            var exceptions = ae.GetInnerExceptions();
            if (exceptions.Any(x => x is SocketException))
            {
                var socketException = (SocketException)exceptions.FirstOrDefault(x => x is SocketException);
                return SocketExceptionHelper.ErrorMessage(socketException);
            }
            else if (exceptions.Any(x => x is PostgresException))
            {
                var dbException = (PostgresException)exceptions.FirstOrDefault(x => x is PostgresException);
                return PostgresExceptionHelper.ErrorMessage(dbException);
            }
            else 
            {
                if (ae.Content != null && IsValidJson(ae.Content))
                {
                    var content = JsonConvert.DeserializeObject<dynamic>(ae.Content);
                    try
                    {
                        return Convert.ToString(content.error.message);
                    }
                    catch
                    {
                        return ae.Message;
                    }
                }
                return ae.Message;
            }
        }

        static bool IsValidJson(string strInput)
        {
            if (string.IsNullOrWhiteSpace(strInput)) { return false; }
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(strInput);
                    return true;
                } 
                catch 
                { 
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
