using Chef.Common.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Npgsql;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace Chef.Common.Exceptions.Helper;

public class ApiClientExceptionHelper
{
    public static ServiceExceptionCode ErrorCode(Refit.ApiException ae)
    {
        return ae.StatusCode switch
        {
            HttpStatusCode.RequestTimeout => ServiceExceptionCode.ApiClientRequestTimeout,
            HttpStatusCode.Unauthorized => ServiceExceptionCode.ApiClientUnauthorized,
            HttpStatusCode.NotFound => ServiceExceptionCode.ApiClientResourceNotFound,
            HttpStatusCode.Continue => throw new NotImplementedException(),
            HttpStatusCode.SwitchingProtocols => throw new NotImplementedException(),
            HttpStatusCode.Processing => throw new NotImplementedException(),
            HttpStatusCode.EarlyHints => throw new NotImplementedException(),
            HttpStatusCode.OK => throw new NotImplementedException(),
            HttpStatusCode.Created => throw new NotImplementedException(),
            HttpStatusCode.Accepted => throw new NotImplementedException(),
            HttpStatusCode.NonAuthoritativeInformation => throw new NotImplementedException(),
            HttpStatusCode.NoContent => throw new NotImplementedException(),
            HttpStatusCode.ResetContent => throw new NotImplementedException(),
            HttpStatusCode.PartialContent => throw new NotImplementedException(),
            HttpStatusCode.MultiStatus => throw new NotImplementedException(),
            HttpStatusCode.AlreadyReported => throw new NotImplementedException(),
            HttpStatusCode.IMUsed => throw new NotImplementedException(),
            HttpStatusCode.Ambiguous => throw new NotImplementedException(),
            HttpStatusCode.Moved => throw new NotImplementedException(),
            HttpStatusCode.Found => throw new NotImplementedException(),
            HttpStatusCode.RedirectMethod => throw new NotImplementedException(),
            HttpStatusCode.NotModified => throw new NotImplementedException(),
            HttpStatusCode.UseProxy => throw new NotImplementedException(),
            HttpStatusCode.Unused => throw new NotImplementedException(),
            HttpStatusCode.RedirectKeepVerb => throw new NotImplementedException(),
            HttpStatusCode.PermanentRedirect => throw new NotImplementedException(),
            HttpStatusCode.BadRequest => throw new NotImplementedException(),
            HttpStatusCode.PaymentRequired => throw new NotImplementedException(),
            HttpStatusCode.Forbidden => throw new NotImplementedException(),
            HttpStatusCode.MethodNotAllowed => throw new NotImplementedException(),
            HttpStatusCode.NotAcceptable => throw new NotImplementedException(),
            HttpStatusCode.ProxyAuthenticationRequired => throw new NotImplementedException(),
            HttpStatusCode.Conflict => throw new NotImplementedException(),
            HttpStatusCode.Gone => throw new NotImplementedException(),
            HttpStatusCode.LengthRequired => throw new NotImplementedException(),
            HttpStatusCode.PreconditionFailed => throw new NotImplementedException(),
            HttpStatusCode.RequestEntityTooLarge => throw new NotImplementedException(),
            HttpStatusCode.RequestUriTooLong => throw new NotImplementedException(),
            HttpStatusCode.UnsupportedMediaType => throw new NotImplementedException(),
            HttpStatusCode.RequestedRangeNotSatisfiable => throw new NotImplementedException(),
            HttpStatusCode.ExpectationFailed => throw new NotImplementedException(),
            HttpStatusCode.MisdirectedRequest => throw new NotImplementedException(),
            HttpStatusCode.UnprocessableEntity => throw new NotImplementedException(),
            HttpStatusCode.Locked => throw new NotImplementedException(),
            HttpStatusCode.FailedDependency => throw new NotImplementedException(),
            HttpStatusCode.UpgradeRequired => throw new NotImplementedException(),
            HttpStatusCode.PreconditionRequired => throw new NotImplementedException(),
            HttpStatusCode.TooManyRequests => throw new NotImplementedException(),
            HttpStatusCode.RequestHeaderFieldsTooLarge => throw new NotImplementedException(),
            HttpStatusCode.UnavailableForLegalReasons => throw new NotImplementedException(),
            HttpStatusCode.InternalServerError => throw new NotImplementedException(),
            HttpStatusCode.NotImplemented => throw new NotImplementedException(),
            HttpStatusCode.BadGateway => throw new NotImplementedException(),
            HttpStatusCode.ServiceUnavailable => throw new NotImplementedException(),
            HttpStatusCode.GatewayTimeout => throw new NotImplementedException(),
            HttpStatusCode.HttpVersionNotSupported => throw new NotImplementedException(),
            HttpStatusCode.VariantAlsoNegotiates => throw new NotImplementedException(),
            HttpStatusCode.InsufficientStorage => throw new NotImplementedException(),
            HttpStatusCode.LoopDetected => throw new NotImplementedException(),
            HttpStatusCode.NotExtended => throw new NotImplementedException(),
            HttpStatusCode.NetworkAuthenticationRequired => throw new NotImplementedException(),
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
            HttpStatusCode.Continue => throw new NotImplementedException(),
            HttpStatusCode.SwitchingProtocols => throw new NotImplementedException(),
            HttpStatusCode.Processing => throw new NotImplementedException(),
            HttpStatusCode.EarlyHints => throw new NotImplementedException(),
            HttpStatusCode.OK => throw new NotImplementedException(),
            HttpStatusCode.Created => throw new NotImplementedException(),
            HttpStatusCode.Accepted => throw new NotImplementedException(),
            HttpStatusCode.NonAuthoritativeInformation => throw new NotImplementedException(),
            HttpStatusCode.NoContent => throw new NotImplementedException(),
            HttpStatusCode.ResetContent => throw new NotImplementedException(),
            HttpStatusCode.PartialContent => throw new NotImplementedException(),
            HttpStatusCode.MultiStatus => throw new NotImplementedException(),
            HttpStatusCode.AlreadyReported => throw new NotImplementedException(),
            HttpStatusCode.IMUsed => throw new NotImplementedException(),
            HttpStatusCode.Ambiguous => throw new NotImplementedException(),
            HttpStatusCode.Moved => throw new NotImplementedException(),
            HttpStatusCode.Found => throw new NotImplementedException(),
            HttpStatusCode.RedirectMethod => throw new NotImplementedException(),
            HttpStatusCode.NotModified => throw new NotImplementedException(),
            HttpStatusCode.UseProxy => throw new NotImplementedException(),
            HttpStatusCode.Unused => throw new NotImplementedException(),
            HttpStatusCode.RedirectKeepVerb => throw new NotImplementedException(),
            HttpStatusCode.PermanentRedirect => throw new NotImplementedException(),
            HttpStatusCode.BadRequest => throw new NotImplementedException(),
            HttpStatusCode.PaymentRequired => throw new NotImplementedException(),
            HttpStatusCode.Forbidden => throw new NotImplementedException(),
            HttpStatusCode.MethodNotAllowed => throw new NotImplementedException(),
            HttpStatusCode.NotAcceptable => throw new NotImplementedException(),
            HttpStatusCode.ProxyAuthenticationRequired => throw new NotImplementedException(),
            HttpStatusCode.Conflict => throw new NotImplementedException(),
            HttpStatusCode.Gone => throw new NotImplementedException(),
            HttpStatusCode.LengthRequired => throw new NotImplementedException(),
            HttpStatusCode.PreconditionFailed => throw new NotImplementedException(),
            HttpStatusCode.RequestEntityTooLarge => throw new NotImplementedException(),
            HttpStatusCode.RequestUriTooLong => throw new NotImplementedException(),
            HttpStatusCode.UnsupportedMediaType => throw new NotImplementedException(),
            HttpStatusCode.RequestedRangeNotSatisfiable => throw new NotImplementedException(),
            HttpStatusCode.ExpectationFailed => throw new NotImplementedException(),
            HttpStatusCode.MisdirectedRequest => throw new NotImplementedException(),
            HttpStatusCode.UnprocessableEntity => throw new NotImplementedException(),
            HttpStatusCode.Locked => throw new NotImplementedException(),
            HttpStatusCode.FailedDependency => throw new NotImplementedException(),
            HttpStatusCode.UpgradeRequired => throw new NotImplementedException(),
            HttpStatusCode.PreconditionRequired => throw new NotImplementedException(),
            HttpStatusCode.TooManyRequests => throw new NotImplementedException(),
            HttpStatusCode.RequestHeaderFieldsTooLarge => throw new NotImplementedException(),
            HttpStatusCode.UnavailableForLegalReasons => throw new NotImplementedException(),
            HttpStatusCode.NotImplemented => throw new NotImplementedException(),
            HttpStatusCode.BadGateway => throw new NotImplementedException(),
            HttpStatusCode.GatewayTimeout => throw new NotImplementedException(),
            HttpStatusCode.HttpVersionNotSupported => throw new NotImplementedException(),
            HttpStatusCode.VariantAlsoNegotiates => throw new NotImplementedException(),
            HttpStatusCode.InsufficientStorage => throw new NotImplementedException(),
            HttpStatusCode.LoopDetected => throw new NotImplementedException(),
            HttpStatusCode.NotExtended => throw new NotImplementedException(),
            HttpStatusCode.NetworkAuthenticationRequired => throw new NotImplementedException(),
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
            dynamic content = JsonConvert.DeserializeObject<dynamic>(ae.Content);

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

    private static string GetInternalServerErrorMessage(Refit.ApiException ae)
    {
        System.Collections.Generic.IEnumerable<Exception> exceptions = ae.GetInnerExceptions();

        if (exceptions.Any(x => x is SocketException))
        {
            SocketException socketException = (SocketException)exceptions.FirstOrDefault(x => x is SocketException);
            return SocketExceptionHelper.ErrorMessage(socketException);
        }
        else if (exceptions.Any(x => x is PostgresException))
        {
            PostgresException dbException = (PostgresException)exceptions.FirstOrDefault(x => x is PostgresException);
            return PostgresExceptionHelper.ErrorMessage(dbException);
        }
        else
        {
            if (ae.Content != null && IsValidJson(ae.Content))
            {
                dynamic content = JsonConvert.DeserializeObject<dynamic>(ae.Content);

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

    private static bool IsValidJson(string strInput)
    {
        if (string.IsNullOrWhiteSpace(strInput)) { return false; }
        strInput = strInput.Trim();

        if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
            (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
        {
            try
            {
                JToken obj = JToken.Parse(strInput);
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
