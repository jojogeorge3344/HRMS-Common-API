namespace Chef.Common.Repositories;

public interface IQueryBuilder<T>
{
    public string TableName { get; }
    public string TableNameWOSchema { get; }
    public string SchemaName { get; }
    string GenerateCreateTableQuery();
    string GenerateInsertQuery(bool returnId = true);
    string GenerateUpdateQuery();
    string GenerateUpdateQueryOnConflict();
    //string GenerateDeleteQuery();
}
