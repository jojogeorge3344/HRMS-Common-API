using Chef.Common.Types;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace Chef.Common.Exceptions.Helper
{
    public class SocketExceptionHelper
    {
        public static ServiceExceptionCode ErrorCode(SocketException se)
        {
            return se.SocketErrorCode switch
            {
                SocketError.TimedOut => ServiceExceptionCode.SocketTimeout,
                SocketError.ConnectionRefused => ServiceExceptionCode.SocketTimeout,
                _ => ServiceExceptionCode.SocketException
            };
        }

        public static string ErrorMessage(SocketException se)
        {
            return se.SocketErrorCode switch
            {
                SocketError.TimedOut => "Operation timed out.",
                SocketError.ConnectionRefused => "Connection refused by remote system.",
                SocketError.AccessDenied => "Access to the resoruce is denied.",
                _ => Regex.Replace(se.SocketErrorCode.ToString(), "(\\B[A-Z])", " $1")

            };
        }
    }

}
