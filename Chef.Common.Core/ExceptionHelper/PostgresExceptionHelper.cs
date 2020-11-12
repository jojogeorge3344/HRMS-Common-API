using Chef.Common.Types;
using Npgsql;

namespace Chef.Common.Exceptions.Helper
{
    public class PostgresExceptionHelper
    {
        public static ServiceExceptionCode ErrorCode(PostgresException pe)
        {
            return pe.SqlState switch
            {
                "23505" => ServiceExceptionCode.DbUniqueKeyViolation,//UniqueConstraintViolation
                "23503" => ServiceExceptionCode.DbForeignKeyViolation,//FK Violation 
                string state when state.StartsWith("0B") => ServiceExceptionCode.DbConnectionException,//Class 08 — Connection Exception
                string state when state.StartsWith("22") => ServiceExceptionCode.DbDataException,//Class 22 — Data Exception
                string state when state.StartsWith("23") => ServiceExceptionCode.DbIntegrityViolation,//Class 23 — Integrity Constraint Violation
                string state when state.StartsWith("40") => ServiceExceptionCode.DbTranasctionRollback,//Tranasction Rollback
                string state when state.StartsWith("25") ||
                state.StartsWith("2D") || state.StartsWith("0B") => ServiceExceptionCode.DbInvalidTranasction,//Class 25 — Invalid Transaction State
                string state when state.StartsWith("26") || state.StartsWith("42") ||
                state.StartsWith("34") || state.StartsWith("3D") || state.StartsWith("3F") => ServiceExceptionCode.DbInvalidSqlStatement,//Class 3F — Invalid Schema Name
                string state when state.StartsWith("53") => ServiceExceptionCode.DbInsufficientResources,//Insufficient Resources
                _ => ServiceExceptionCode.DbException,
            }; 
        }

        public static string ErrorMessage(PostgresException pe)
        {
            return pe.SqlState switch
            {
                "23505" => "Record with same properties already exists.",//UniqueConstraintViolation
                "23503" => "This operation violates data integrity constraints.",//FK Violation 
                string state when state.StartsWith("0B") => "This operation violates data integrity constraints.",//Class 08 — Connection Exception
                string state when state.StartsWith("22") => "Invalid data.",//Class 22 — Data Exception
                string state when state.StartsWith("23") => "This operation violates data integrity constraints.",//Class 23 — Integrity Constraint Violation
                string state when state.StartsWith("40") => "Transaction failed and rollbacked.",//Tranasction Rollback
                string state when state.StartsWith("25") ||
                state.StartsWith("2D") || state.StartsWith("0B") => "Transaction failed and rollbacked.",//Class 25 — Invalid Transaction State
                string state when state.StartsWith("26") || state.StartsWith("42") ||
                state.StartsWith("34") || state.StartsWith("3D") || state.StartsWith("3F") => "Invalid database command. Check syntax!",//Class 3F — Invalid Schema Name
                string state when state.StartsWith("53") => "Insufficient resource to perform this operation.",//Insufficient Resources
                _ => "Database operation failed.",

            };
        }
    }
}
