namespace Chef.Common.DMS.Repositories;

public class FileMetaDataRepository : TenantRepository<FileMetaData>, IFileMetaDataRepository
{
    public FileMetaDataRepository(
        IHttpContextAccessor httpContextAccessor,
        ITenantConnectionFactory connectionFactory)
        : base(httpContextAccessor, connectionFactory)
    {
    }

    public async Task<FileMetaData> GetByFileId(int fileId)
    {
        string sql = @"select * from dms.filemetadata where fileid=@fileId";
        return await Connection.QueryFirstOrDefaultAsync<FileMetaData>(sql,new { fileId });

    }
}
