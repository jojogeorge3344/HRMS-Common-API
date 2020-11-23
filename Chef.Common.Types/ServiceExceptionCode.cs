namespace Chef.Common.Types
{
    public enum ServiceExceptionCode
    {
        // Database Exception
        DbConnectionException,
        DbTimeoutException,
        DbUniqueKeyViolation,
        DbForeignKeyViolation,
        DbTranasctionRollback,
        DbDataException,
        DbInvalidTranasction,
        DbInvalidSqlStatement,
        DbInsufficientResources,
        DbIntegrityViolation ,
        DbException,

        // Socket Exception
        SocketException,
        SocketTimeout,
        SocketConnectionRefused,

        //Api Exception
        ApiClientResourceNotFound,
        ApiClientInternalServerError,
        ApiClientUnauthorized, 
        ApiClientRequestTimeout,

        //
        ResourceNotFound,
        TenantNotFound
    }
}
