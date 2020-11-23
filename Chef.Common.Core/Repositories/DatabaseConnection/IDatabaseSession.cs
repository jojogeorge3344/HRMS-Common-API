using Chef.Common.Core;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Chef.Common.Repositories
{
    public interface IDatabaseSession
    {
        /// <summary>
        ///This is a Unit of work session block. It applies transaction to all the database commands included within this block. 
        ///This block supersedes any other session block declare inside that and combines everything into one block.
        /// </summary>
        /// <param name="isolationLevel">Transaction Isolation Level</param>
        /// <returns>IUnitOfWorkSession</returns>
        IUnitOfWorkSession UnitOfWorkSession(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
        IUserToken UserToken { get; }

        #region Execute Scalar Async
        Task<TOutput> ExecuteScalarAsync<TOutput>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);
        Task<TOutput> ExecuteScalarAsync<TOutput>(SqlKata.Query query, CancellationToken cancellationToken = default);

        #endregion

        #region Execute Scalar
        TOutput ExecuteScalar<TOutput>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered);
        TOutput ExecuteScalar<TOutput>(SqlKata.Query query);

        #endregion

        #region Execute Async
        Task<int> ExecuteAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);
        Task<int> ExecuteAsync(SqlKata.Query query, CancellationToken cancellationToken = default);
        #endregion

        #region Execute
        int Execute(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered);
        int Execute(SqlKata.Query query);
        #endregion

        #region Execute Reader
        Task<IDataReader> ExecuteReaderAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);
        IDataReader ExecuteReader(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);

        #endregion

        #region Query Single Async
        Task<TOutput> QuerySingleOrDefaultAsync<TOutput>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);
        Task<TOutput> QuerySingleAsync<TOutput>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);
        #endregion

        #region Query Single
        TOutput QuerySingleOrDefault<TOutput>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered);
        TOutput QuerySingle<TOutput>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered);
        #endregion

        #region Query First Async
        Task<TOutput> QueryFirstOrDefaultAsync<TOutput>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);
        Task<TOutput> QueryFirstAsync<TOutput>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);
        Task<TOutput> QueryFirstOrDefaultAsync<TOutput>(SqlKata.Query query, CancellationToken cancellationToken = default);
        Task<TOutput> QueryFirstAsync<TOutput>(SqlKata.Query query, CancellationToken cancellationToken = default);

        #endregion

        #region Query First
        TOutput QueryFirstOrDefault<TOutput>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered);
        TOutput QueryFirst<TOutput>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered);
        TOutput QueryFirstOrDefault<TOutput>(SqlKata.Query query);
        TOutput QueryFirst<TOutput>(SqlKata.Query query);

        #endregion

        #region QueryAsync
        Task<IEnumerable<TOutput>> QueryAsync<TOutput>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);
        Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null,
           string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);

        Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null,
          string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);

        Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null,
            string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);

        Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, object param = null,
            string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);
        Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, object param = null,
            string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);
        Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, object param = null,
            string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);
        Task<IEnumerable<dynamic>> QueryAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);
        Task<IEnumerable<dynamic>> QueryAsync(string sql, Type[] types, Func<object[], dynamic> map, object param = null,
            string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered);

        Task<IEnumerable<TOutput>> QueryAsync<TOutput>(SqlKata.Query query, CancellationToken cancellationToken = default);

        #endregion

        #region Query
        IEnumerable<TOutput> Query<TOutput>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered);
        IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null,
           string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered);
        IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null,
           string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered);
        IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null,
           string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered);
        IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, object param = null,
           string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered);
        IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, object param = null,
           string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered);
        IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, object param = null,
           string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered);
        IEnumerable<dynamic> Query(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered);
        IEnumerable<TOutput> Query<TOutput>(SqlKata.Query query, CommandFlags flags = CommandFlags.Buffered);

        #endregion

        #region Query Mutliple
        Task<GridReader> QueryMultipleAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);
        GridReader QueryMultiple(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered);
        #endregion
    }
}
