namespace Chef.Common.Repositories
{
    ///<summary>
    ///This is a Generic repository function. It will handle all the generic database repository actions such as Get, Getall, Insert, Update and Delete
    //////</summary>
    //public interface IDatabaseCommand2
    //{
    //    #region Execute Scalar
    //    Task<TOutput> ExecuteScalarAsync<TOutput>(string sql, object param = null,int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);
    //    TOutput ExecuteScalar<TOutput>(string sql, object param = null,int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);
    //    #endregion

    //    #region Execute
    //    Task<int> ExecuteAsync(string sql, object param = null,int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);
    //    int Execute(string sql, object param = null,int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);
    //    #endregion

    //    #region Execute Reader
    //    Task<IDataReader> ExecuteReaderAsync(string sql, object param = null,int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);
    //    IDataReader ExecuteReader(string sql, object param = null,int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);

    //    #endregion

    //    #region Query Single
    //    Task<TOutput> QuerySingleOrDefaultAsync<TOutput>(string sql, object param = null,int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);
    //    Task<TOutput> QuerySingleAsync<TOutput>(string sql, object param = null,int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);

    //    TOutput QuerySingleOrDefault<TOutput>(string sql, object param = null,int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);
    //    TOutput QuerySingle<TOutput>(string sql, object param = null,int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);
    //    #endregion

    //    #region Query First
    //    Task<TOutput> QueryFirstOrDefaultAsync<TOutput>(string sql, object param = null,int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);
    //    Task<TOutput> QueryFirstAsync<TOutput>(string sql, object param = null,int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);

    //    TOutput QueryFirstOrDefault<TOutput>(string sql, object param = null,int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);
    //    TOutput QueryFirst<TOutput>(string sql, object param = null,int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);
    //    #endregion

    //    #region QueryAsync
    //    Task<IEnumerable<TOutput>> QueryAsync<TOutput>(string sql, object param = null,int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);
    //    Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null,
    //       string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);

    //    Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null,
    //      string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);

    //    Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null,
    //        string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);

    //    Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, object param = null,
    //        string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);
    //    Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, object param = null,
    //        string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);
    //    Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, object param = null,
    //        string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);
    //    Task<IEnumerable<dynamic>> QueryAsync(string sql, object param = null,int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);
    //    #endregion

    //    #region Query
    //    IEnumerable<TOutput> Query<TOutput>(string sql, object param = null,int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered);
    //    IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null,
    //       string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered);
    //    IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null,
    //       string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered);
    //    IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null,
    //       string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered);
    //    IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, object param = null,
    //       string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered);
    //    IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, object param = null,
    //       string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered);
    //    IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, object param = null,
    //       string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered);
    //    IEnumerable<dynamic> Query(string sql, object param = null,int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);
    //    #endregion

    //    #region Query Mutliple
    //    Task<GridReader> QueryMultipleAsync(string sql, object param = null,int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);
    //    GridReader QueryMultiple(string sql, object param = null,int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);
    //    #endregion
    //}
}
