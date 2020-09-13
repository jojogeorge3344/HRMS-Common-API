namespace Chef.Common.Repositories
{
    //internal class DatabaseCommand2 : IDatabaseCommand2
    //{
    //    //string commandText;
    //    //object parameters = null;
    //    //int? commandTimeout = null;
    //    //CommandType? commandType = null;
    //    //CommandFlags flags = CommandFlags.Buffered;
    //    //CommandDefinition commandDefinition;
    //    public IDbConnection Connection { get; set; }
    //    public IDbTransaction Transaction { get; set; }
    //    //public DatabaseCommand Command(CommandDefinition commandDefinition)
    //    //{
    //    //    this.commandText = commandDefinition.CommandText;
    //    //    this.parameters = commandDefinition.Parameters;
    //    //    //this.Transaction = commandDefinition.Transaction;
    //    //    this.commandTimeout = commandDefinition.CommandTimeout;
    //    //    this.commandType = commandDefinition.CommandType;
    //    //    this.flags = commandDefinition.Flags;
    //    //    return this;
    //    //}

    //    //public DatabaseCommand Command(string sql, object param = null,
    //    //    int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered)
    //    //{
    //    //    this.commandText = sql;
    //    //    this.parameters = param;
    //    //    //this.Transaction = transaction;
    //    //    this.commandTimeout = commandTimeout;
    //    //    this.commandType = commandType;
    //    //    this.flags = flags;
    //    //    return this; 
    //    //}


    //    #region Execute Scalar
    //    public Task<TOutput> ExecuteScalarAsync<TOutput>(string sql, object param = null,int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) =>
    //        Connection.ExecuteScalarAsync<TOutput>(new Dapper.CommandDefinition(sql, param, Transaction, commandTimeout, commandType, flags, cancellationToken));
    //    public TOutput ExecuteScalar<TOutput>(string sql, object param = null,int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) =>
    //        Connection.ExecuteScalar<TOutput>(new Dapper.CommandDefinition(sql, param, Transaction, commandTimeout, commandType, flags, cancellationToken));

    //    #endregion

    //    #region Execute Reader
    //    public Task<IDataReader> ExecuteReaderAsync(string sql, object param = null,int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) =>
    //       Connection.ExecuteReaderAsync(new Dapper.CommandDefinition(sql, param, Transaction, commandTimeout, commandType, flags, cancellationToken));

    //    public IDataReader ExecuteReader(string sql, object param = null,int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) =>
    //       Connection.ExecuteReader(new Dapper.CommandDefinition(sql, param, Transaction, commandTimeout, commandType, flags, cancellationToken));

    //    #endregion

    //    #region Execute 
    //    public Task<int> ExecuteAsync(string sql, object param = null,int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) =>
    //        Connection.ExecuteAsync(new Dapper.CommandDefinition(sql, param, Transaction, commandTimeout, commandType, flags, cancellationToken));
    //    public int Execute(string sql, object param = null,int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) =>
    //        Connection.Execute(new Dapper.CommandDefinition(sql, param, Transaction, commandTimeout, commandType, flags, cancellationToken));

    //    #endregion

    //    #region Query First
    //    public Task<TOutput> QueryFirstOrDefaultAsync<TOutput>(string sql, object param = null,int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) =>
    //        Connection.QueryFirstOrDefaultAsync<TOutput>(new Dapper.CommandDefinition(sql, param, Transaction, commandTimeout, commandType, flags, cancellationToken));
    //    public Task<TOutput> QueryFirstAsync<TOutput>(string sql, object param = null,int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) =>
    //        Connection.QueryFirstAsync<TOutput>(new Dapper.CommandDefinition(sql, param, Transaction, commandTimeout, commandType, flags, cancellationToken));
    //    public TOutput QueryFirstOrDefault<TOutput>(string sql, object param = null,int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) =>
    //        Connection.QueryFirstOrDefault<TOutput>(new Dapper.CommandDefinition(sql, param, Transaction, commandTimeout, commandType, flags, cancellationToken));
    //    public TOutput QueryFirst<TOutput>(string sql, object param = null,int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) =>
    //        Connection.QueryFirst<TOutput>(new Dapper.CommandDefinition(sql, param, Transaction, commandTimeout, commandType, flags, cancellationToken));

    //    #endregion

    //    #region Query Single
    //    public Task<TOutput> QuerySingleOrDefaultAsync<TOutput>(string sql, object param = null,int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) =>
    //       Connection.QuerySingleOrDefaultAsync<TOutput>(new Dapper.CommandDefinition(sql, param, Transaction, commandTimeout, commandType, flags, cancellationToken));
    //    public Task<TOutput> QuerySingleAsync<TOutput>(string sql, object param = null,int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) =>
    //        Connection.QuerySingleAsync<TOutput>(new Dapper.CommandDefinition(sql, param, Transaction, commandTimeout, commandType, flags, cancellationToken));
    //    public TOutput QuerySingleOrDefault<TOutput>(string sql, object param = null,int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) =>
    //        Connection.QuerySingleOrDefault<TOutput>(new Dapper.CommandDefinition(sql, param, Transaction, commandTimeout, commandType, flags, cancellationToken));
    //    public TOutput QuerySingle<TOutput>(string sql, object param = null,int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) =>
    //        Connection.QuerySingle<TOutput>(new Dapper.CommandDefinition(sql, param, Transaction, commandTimeout, commandType, flags, cancellationToken));

    //    #endregion

    //    #region Query Async 
    //    public Task<IEnumerable<TOutput>> QueryAsync<TOutput>(string sql, object param = null,int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) =>
    //        Connection.QueryAsync<TOutput>(new Dapper.CommandDefinition(sql, param, Transaction, commandTimeout, commandType, flags, cancellationToken));

    //    public Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null,
    //       string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) =>
    //       Connection.QueryAsync(new Dapper.CommandDefinition(sql, param, Transaction, commandTimeout, commandType, flags, cancellationToken),
    //       map: map, splitOn: splitOn);

    //    public Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null,
    //      string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) =>
    //      Connection.QueryAsync(new Dapper.CommandDefinition(sql, param, Transaction, commandTimeout, commandType, flags, cancellationToken),
    //      map: map, splitOn: splitOn);

    //    public Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null,
    //        string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) =>
    //        Connection.QueryAsync(new Dapper.CommandDefinition(sql, param, Transaction, commandTimeout, commandType, flags, cancellationToken),
    //        map: map, splitOn: splitOn);

    //    public Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, object param = null,
    //        string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) =>
    //        Connection.QueryAsync(new Dapper.CommandDefinition(sql, param, Transaction, commandTimeout, commandType, flags, cancellationToken),
    //        map: map, splitOn: splitOn);
    //    public Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, object param = null,
    //        string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) =>
    //        Connection.QueryAsync(new Dapper.CommandDefinition(sql, param, Transaction, commandTimeout, commandType, flags, cancellationToken),
    //        map: map, splitOn: splitOn);
    //    public Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, object param = null,
    //        string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) => 
    //        Connection.QueryAsync(new Dapper.CommandDefinition(sql, param, Transaction, commandTimeout, commandType, flags, cancellationToken),
    //            map: map, splitOn: splitOn);
    //    public Task<IEnumerable<dynamic>> QueryAsync(string sql, object param = null,int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) => 
    //        Connection.QueryAsync<dynamic>(new Dapper.CommandDefinition(sql, param, Transaction, commandTimeout, commandType, flags, cancellationToken));

    //    #endregion

    //    #region Query
    //    //Sync
    //    public IEnumerable<TOutput> Query<TOutput>(string sql, object param = null,int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered) =>
    //        Connection.Query<TOutput>(new Dapper.CommandDefinition(sql, param, Transaction, commandTimeout, commandType, flags));

    //    public IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null,
    //       string splitOn = "Id",int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered) =>
    //       Connection.Query(sql: sql, map: map, splitOn: splitOn, param: param,
    //           transaction: Transaction, commandTimeout: commandTimeout, commandType: commandType);

    //    public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null,
    //       string splitOn = "Id",int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered) =>
    //       Connection.Query(sql: sql, map: map, splitOn: splitOn, param: param,
    //           transaction: Transaction, commandTimeout: commandTimeout, commandType: commandType);
    //    public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null,
    //       string splitOn = "Id",int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered) =>
    //       Connection.Query(sql: sql, map: map, splitOn: splitOn, param: param,
    //           transaction: Transaction, commandTimeout: commandTimeout, commandType: commandType);
    //    public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, object param = null,
    //       string splitOn = "Id",int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered) =>
    //       Connection.Query(sql: sql, map: map, splitOn: splitOn, param: param,
    //           transaction: Transaction, commandTimeout: commandTimeout, commandType: commandType);
    //    public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, object param = null,
    //       string splitOn = "Id",int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered) =>
    //       Connection.Query(sql: sql, map: map, splitOn: splitOn, param: param,
    //           transaction: Transaction, commandTimeout: commandTimeout, commandType: commandType);
    //    public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, object param = null,
    //       string splitOn = "Id",int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered) =>
    //       Connection.Query(sql: sql, map: map, splitOn: splitOn, param: param,
    //           transaction: Transaction, commandTimeout: commandTimeout, commandType: commandType);
    //    public IEnumerable<dynamic> Query(string sql, object param = null,int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) =>
    //        Connection.Query<dynamic>(new Dapper.CommandDefinition(sql, param, Transaction, commandTimeout, commandType, flags, cancellationToken));

    //    #endregion

    //    #region Query Mutiple  
    //    public Task<GridReader> QueryMultipleAsync(string sql, object param = null,int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) =>
    //        Connection.QueryMultipleAsync(new Dapper.CommandDefinition(sql, param, Transaction, commandTimeout, commandType, flags, cancellationToken));

    //    public GridReader QueryMultiple(string sql, object param = null,int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) =>
    //        Connection.QueryMultiple(new Dapper.CommandDefinition(sql, param, Transaction, commandTimeout, commandType, flags, cancellationToken));

    //    #endregion


    //}


}
