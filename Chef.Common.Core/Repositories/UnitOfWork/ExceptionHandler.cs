using Npgsql;
using System;
using System.Linq;

namespace Chef.Common.Core.Repositories.UnitOfWork
{
    public class ExceptionHandler : IExceptionHandler
    {
        private static readonly int[] RetryErrorCodes =
        {
            20,    // The instance of SQL Server you attempted to connect to does not support encryption. 
        };
        public bool Retry(Exception ex)
        {
            if (!(ex is NpgsqlException sqlException))
                //return ex is TimeoutException;
                return false;
            return RetryErrorCodes.Contains(sqlException.ErrorCode);
        }
    }
}
