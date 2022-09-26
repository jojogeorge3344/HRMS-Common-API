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
                SocketError.SocketError => throw new System.NotImplementedException(),
                SocketError.Success => throw new System.NotImplementedException(),
                SocketError.OperationAborted => throw new System.NotImplementedException(),
                SocketError.IOPending => throw new System.NotImplementedException(),
                SocketError.Interrupted => throw new System.NotImplementedException(),
                SocketError.AccessDenied => throw new System.NotImplementedException(),
                SocketError.Fault => throw new System.NotImplementedException(),
                SocketError.InvalidArgument => throw new System.NotImplementedException(),
                SocketError.TooManyOpenSockets => throw new System.NotImplementedException(),
                SocketError.WouldBlock => throw new System.NotImplementedException(),
                SocketError.InProgress => throw new System.NotImplementedException(),
                SocketError.AlreadyInProgress => throw new System.NotImplementedException(),
                SocketError.NotSocket => throw new System.NotImplementedException(),
                SocketError.DestinationAddressRequired => throw new System.NotImplementedException(),
                SocketError.MessageSize => throw new System.NotImplementedException(),
                SocketError.ProtocolType => throw new System.NotImplementedException(),
                SocketError.ProtocolOption => throw new System.NotImplementedException(),
                SocketError.ProtocolNotSupported => throw new System.NotImplementedException(),
                SocketError.SocketNotSupported => throw new System.NotImplementedException(),
                SocketError.OperationNotSupported => throw new System.NotImplementedException(),
                SocketError.ProtocolFamilyNotSupported => throw new System.NotImplementedException(),
                SocketError.AddressFamilyNotSupported => throw new System.NotImplementedException(),
                SocketError.AddressAlreadyInUse => throw new System.NotImplementedException(),
                SocketError.AddressNotAvailable => throw new System.NotImplementedException(),
                SocketError.NetworkDown => throw new System.NotImplementedException(),
                SocketError.NetworkUnreachable => throw new System.NotImplementedException(),
                SocketError.NetworkReset => throw new System.NotImplementedException(),
                SocketError.ConnectionAborted => throw new System.NotImplementedException(),
                SocketError.ConnectionReset => throw new System.NotImplementedException(),
                SocketError.NoBufferSpaceAvailable => throw new System.NotImplementedException(),
                SocketError.IsConnected => throw new System.NotImplementedException(),
                SocketError.NotConnected => throw new System.NotImplementedException(),
                SocketError.Shutdown => throw new System.NotImplementedException(),
                SocketError.HostDown => throw new System.NotImplementedException(),
                SocketError.HostUnreachable => throw new System.NotImplementedException(),
                SocketError.ProcessLimit => throw new System.NotImplementedException(),
                SocketError.SystemNotReady => throw new System.NotImplementedException(),
                SocketError.VersionNotSupported => throw new System.NotImplementedException(),
                SocketError.NotInitialized => throw new System.NotImplementedException(),
                SocketError.Disconnecting => throw new System.NotImplementedException(),
                SocketError.TypeNotFound => throw new System.NotImplementedException(),
                SocketError.HostNotFound => throw new System.NotImplementedException(),
                SocketError.TryAgain => throw new System.NotImplementedException(),
                SocketError.NoRecovery => throw new System.NotImplementedException(),
                SocketError.NoData => throw new System.NotImplementedException(),
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
                SocketError.SocketError => throw new System.NotImplementedException(),
                SocketError.Success => throw new System.NotImplementedException(),
                SocketError.OperationAborted => throw new System.NotImplementedException(),
                SocketError.IOPending => throw new System.NotImplementedException(),
                SocketError.Interrupted => throw new System.NotImplementedException(),
                SocketError.Fault => throw new System.NotImplementedException(),
                SocketError.InvalidArgument => throw new System.NotImplementedException(),
                SocketError.TooManyOpenSockets => throw new System.NotImplementedException(),
                SocketError.WouldBlock => throw new System.NotImplementedException(),
                SocketError.InProgress => throw new System.NotImplementedException(),
                SocketError.AlreadyInProgress => throw new System.NotImplementedException(),
                SocketError.NotSocket => throw new System.NotImplementedException(),
                SocketError.DestinationAddressRequired => throw new System.NotImplementedException(),
                SocketError.MessageSize => throw new System.NotImplementedException(),
                SocketError.ProtocolType => throw new System.NotImplementedException(),
                SocketError.ProtocolOption => throw new System.NotImplementedException(),
                SocketError.ProtocolNotSupported => throw new System.NotImplementedException(),
                SocketError.SocketNotSupported => throw new System.NotImplementedException(),
                SocketError.OperationNotSupported => throw new System.NotImplementedException(),
                SocketError.ProtocolFamilyNotSupported => throw new System.NotImplementedException(),
                SocketError.AddressFamilyNotSupported => throw new System.NotImplementedException(),
                SocketError.AddressAlreadyInUse => throw new System.NotImplementedException(),
                SocketError.AddressNotAvailable => throw new System.NotImplementedException(),
                SocketError.NetworkDown => throw new System.NotImplementedException(),
                SocketError.NetworkUnreachable => throw new System.NotImplementedException(),
                SocketError.NetworkReset => throw new System.NotImplementedException(),
                SocketError.ConnectionAborted => throw new System.NotImplementedException(),
                SocketError.ConnectionReset => throw new System.NotImplementedException(),
                SocketError.NoBufferSpaceAvailable => throw new System.NotImplementedException(),
                SocketError.IsConnected => throw new System.NotImplementedException(),
                SocketError.NotConnected => throw new System.NotImplementedException(),
                SocketError.Shutdown => throw new System.NotImplementedException(),
                SocketError.HostDown => throw new System.NotImplementedException(),
                SocketError.HostUnreachable => throw new System.NotImplementedException(),
                SocketError.ProcessLimit => throw new System.NotImplementedException(),
                SocketError.SystemNotReady => throw new System.NotImplementedException(),
                SocketError.VersionNotSupported => throw new System.NotImplementedException(),
                SocketError.NotInitialized => throw new System.NotImplementedException(),
                SocketError.Disconnecting => throw new System.NotImplementedException(),
                SocketError.TypeNotFound => throw new System.NotImplementedException(),
                SocketError.HostNotFound => throw new System.NotImplementedException(),
                SocketError.TryAgain => throw new System.NotImplementedException(),
                SocketError.NoRecovery => throw new System.NotImplementedException(),
                SocketError.NoData => throw new System.NotImplementedException(),
                _ => Regex.Replace(se.SocketErrorCode.ToString(), "(\\B[A-Z])", " $1")

            };
        }
    }

}
