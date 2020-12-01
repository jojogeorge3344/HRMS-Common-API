using Chef.Common.Core;
using Dapper;
using SqlKata;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Chef.Common.Repositories
{
    public class DatabaseSession : IDatabaseSession
    {
        readonly UnitOfWork unitOfWork;
        readonly IDbConnection connection;
        readonly IDbTransaction transaction;
        public IUserToken UserToken { get; private set; }

        public DatabaseSession(IConnectionFactory connectionFactory, IUserToken userToken)
        {
            this.unitOfWork = new UnitOfWork(connectionFactory.Connection);
            connection = unitOfWork.Connection;
            transaction = unitOfWork.Transaction;
            this.UserToken = userToken;
        }
        public DatabaseSession(IConnectionFactory connectionFactory)
        {
            this.unitOfWork = new UnitOfWork(connectionFactory.Connection);
            connection = unitOfWork.Connection;
            transaction = unitOfWork.Transaction;
        }
        public IUnitOfWorkSession UnitOfWorkSession(System.Data.IsolationLevel isolationLevel = System.Data.IsolationLevel.ReadCommitted)
          => new UnitOfWorkSession(unitOfWork, isolationLevel);


        #region Execute Scalar Async
        public Task<TOutput> ExecuteScalarAsync<TOutput>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) =>
            connection.ExecuteScalarAsync<TOutput>(new Dapper.CommandDefinition(sql, param, transaction, commandTimeout, commandType, flags, cancellationToken));
        public Task<TOutput> ExecuteScalarAsync<TOutput>(Query query, CancellationToken cancellationToken = default)
        {
            var sqlResult = query.Compile();
            return ExecuteScalarAsync<TOutput>(sql: sqlResult.Sql, param: sqlResult.NamedBindings, cancellationToken: cancellationToken);
        }

        #endregion

        #region Execute Scalar
        public TOutput ExecuteScalar<TOutput>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered) =>
            connection.ExecuteScalar<TOutput>(new Dapper.CommandDefinition(sql, param, transaction, commandTimeout, commandType, flags));

        public TOutput ExecuteScalar<TOutput>(Query query)
        {
            var sqlResult = query.Compile();
            return ExecuteScalar<TOutput>(sql: sqlResult.Sql, param: sqlResult.NamedBindings);
        }
        #endregion

        #region Execute Reader
        public Task<IDataReader> ExecuteReaderAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) =>
           connection.ExecuteReaderAsync(new Dapper.CommandDefinition(sql, param, transaction, commandTimeout, commandType, flags, cancellationToken));

        public IDataReader ExecuteReader(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) =>
           connection.ExecuteReader(new Dapper.CommandDefinition(sql, param, transaction, commandTimeout, commandType, flags, cancellationToken));

        #endregion

        #region Execute Async
        public Task<int> ExecuteAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) =>
            connection.ExecuteAsync(new Dapper.CommandDefinition(sql, param, transaction, commandTimeout, commandType, flags, cancellationToken));
        public Task<int> ExecuteAsync(Query query, CancellationToken cancellationToken = default)
        {
            var sqlResult = query.Compile();
            return ExecuteAsync(sql: sqlResult.Sql, param: sqlResult.NamedBindings, cancellationToken: cancellationToken);
        }
        #endregion

        #region Execute 
        public int Execute(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered) =>
            connection.Execute(new Dapper.CommandDefinition(sql, param, transaction, commandTimeout, commandType, flags));

        public int Execute(Query query)
        {
            var sqlResult = query.Compile();
            return Execute(sql: sqlResult.Sql, param: sqlResult.NamedBindings);
        }
        #endregion

        #region Query First Async
        public Task<TOutput> QueryFirstOrDefaultAsync<TOutput>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) =>
            connection.QueryFirstOrDefaultAsync<TOutput>(new Dapper.CommandDefinition(sql, param, transaction, commandTimeout, commandType, flags, cancellationToken));
        public Task<TOutput> QueryFirstAsync<TOutput>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) =>
            connection.QueryFirstAsync<TOutput>(new Dapper.CommandDefinition(sql, param, transaction, commandTimeout, commandType, flags, cancellationToken));
        public Task<TOutput> QueryFirstOrDefaultAsync<TOutput>(Query query, CancellationToken cancellationToken = default)
        {
            var sqlResult = query.Compile();
            return QueryFirstOrDefaultAsync<TOutput>(sql: sqlResult.Sql, param: sqlResult.NamedBindings, cancellationToken: cancellationToken);
        }

        public Task<TOutput> QueryFirstAsync<TOutput>(Query query, CancellationToken cancellationToken = default)
        {
            var sqlResult = query.Compile();
            return QueryFirstAsync<TOutput>(sql: sqlResult.Sql, param: sqlResult.NamedBindings, cancellationToken: cancellationToken);
        }

        #endregion

        #region Query First
        public TOutput QueryFirstOrDefault<TOutput>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered) =>
            connection.QueryFirstOrDefault<TOutput>(new Dapper.CommandDefinition(sql, param, transaction, commandTimeout, commandType, flags));
        public TOutput QueryFirst<TOutput>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered) =>
            connection.QueryFirst<TOutput>(new Dapper.CommandDefinition(sql, param, transaction, commandTimeout, commandType, flags));

        public TOutput QueryFirstOrDefault<TOutput>(Query query)
        {
            var sqlResult = query.Compile();
            return QueryFirstOrDefault<TOutput>(sql: sqlResult.Sql, param: sqlResult.NamedBindings);
        }

        public TOutput QueryFirst<TOutput>(Query query)
        {
            var sqlResult = query.Compile();
            return QueryFirst<TOutput>(sql: sqlResult.Sql, param: sqlResult.NamedBindings);
        }

        #endregion

        #region Query Single Async
        public Task<TOutput> QuerySingleOrDefaultAsync<TOutput>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) =>
           connection.QuerySingleOrDefaultAsync<TOutput>(new Dapper.CommandDefinition(sql, param, transaction, commandTimeout, commandType, flags, cancellationToken));
        public Task<TOutput> QuerySingleAsync<TOutput>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) =>
            connection.QuerySingleAsync<TOutput>(new Dapper.CommandDefinition(sql, param, transaction, commandTimeout, commandType, flags, cancellationToken));

        #endregion

        #region Query Single
        public TOutput QuerySingleOrDefault<TOutput>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered) =>
            connection.QuerySingleOrDefault<TOutput>(new Dapper.CommandDefinition(sql, param, transaction, commandTimeout, commandType, flags));
        public TOutput QuerySingle<TOutput>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered) =>
            connection.QuerySingle<TOutput>(new Dapper.CommandDefinition(sql, param, transaction, commandTimeout, commandType, flags));

        #endregion

        #region Query Async 
        public Task<IEnumerable<TOutput>> QueryAsync<TOutput>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) =>
            connection.QueryAsync<TOutput>(new Dapper.CommandDefinition(sql, param, transaction, commandTimeout, commandType, flags, cancellationToken));
        public Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null,
          string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) =>
          connection.QueryAsync(new Dapper.CommandDefinition(sql, param, transaction, commandTimeout, commandType, flags, cancellationToken),
          map: map, splitOn: splitOn);
        public Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null,
          string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) =>
          connection.QueryAsync(new Dapper.CommandDefinition(sql, param, transaction, commandTimeout, commandType, flags, cancellationToken),
          map: map, splitOn: splitOn);

        public Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null,
            string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) =>
            connection.QueryAsync(new Dapper.CommandDefinition(sql, param, transaction, commandTimeout, commandType, flags, cancellationToken),
            map: map, splitOn: splitOn);

        public Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, object param = null,
            string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) =>
            connection.QueryAsync(new Dapper.CommandDefinition(sql, param, transaction, commandTimeout, commandType, flags, cancellationToken),
            map: map, splitOn: splitOn);
        public Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, object param = null,
            string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) =>
            connection.QueryAsync(new Dapper.CommandDefinition(sql, param, transaction, commandTimeout, commandType, flags, cancellationToken),
            map: map, splitOn: splitOn);
        public Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, object param = null,
            string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) =>
            connection.QueryAsync(new Dapper.CommandDefinition(sql, param, transaction, commandTimeout, commandType, flags, cancellationToken),
                map: map, splitOn: splitOn);
        public Task<IEnumerable<dynamic>> QueryAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) =>
            connection.QueryAsync<dynamic>(new Dapper.CommandDefinition(sql, param, transaction, commandTimeout, commandType, flags, cancellationToken));

        public Task<IEnumerable<dynamic>> QueryAsync(string sql, Type[] types, Func<object[], dynamic> map, object param = null,
            string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered) =>
           connection.QueryAsync<dynamic>(sql: sql, types: types, map: map, param, transaction, splitOn: splitOn, commandTimeout: commandTimeout, commandType: commandType);

        public Task<IEnumerable<TOutput>> QueryAsync<TOutput>(Query query, CancellationToken cancellationToken = default)
        {
            var sqlResult = query.Compile();
            return QueryAsync<TOutput>(sql: sqlResult.Sql, param: sqlResult.NamedBindings, cancellationToken: cancellationToken);
        }


        #endregion

        #region Query
        //Sync
        public IEnumerable<TOutput> Query<TOutput>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered) =>
            connection.Query<TOutput>(new Dapper.CommandDefinition(sql, param, transaction, commandTimeout, commandType, flags));

        public IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null,
           string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered) =>
           connection.Query(sql: sql, map: map, splitOn: splitOn, param: param,
               transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);

        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null,
           string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered) =>
           connection.Query(sql: sql, map: map, splitOn: splitOn, param: param,
               transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null,
           string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered) =>
           connection.Query(sql: sql, map: map, splitOn: splitOn, param: param,
               transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, object param = null,
           string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered) =>
           connection.Query(sql: sql, map: map, splitOn: splitOn, param: param,
               transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, object param = null,
           string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered) =>
           connection.Query(sql: sql, map: map, splitOn: splitOn, param: param,
               transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, object param = null,
           string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered) =>
           connection.Query(sql: sql, map: map, splitOn: splitOn, param: param,
               transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
        public IEnumerable<dynamic> Query(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered) =>
            connection.Query<dynamic>(new Dapper.CommandDefinition(sql, param, transaction, commandTimeout, commandType, flags));

        public IEnumerable<TOutput> Query<TOutput>(Query query, CommandFlags flags = CommandFlags.Buffered)
        {
            var sqlResult = query.Compile();
            return Query<TOutput>(sql: sqlResult.Sql, param: sqlResult.NamedBindings);
        }
        #endregion

        #region Query Mutiple  
        public Task<GridReader> QueryMultipleAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) =>
            connection.QueryMultipleAsync(new Dapper.CommandDefinition(sql, param, transaction, commandTimeout, commandType, flags, cancellationToken));

        public GridReader QueryMultiple(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered) =>
            connection.QueryMultiple(new Dapper.CommandDefinition(sql, param, transaction, commandTimeout, commandType, flags));



        #endregion

    }
}
