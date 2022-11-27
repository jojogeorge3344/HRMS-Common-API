namespace Chef.Common.DMS.Repositories;

public class FileVersionRepository : TenantRepository<FileVersion>, IFileVersionRepository
{
    public FileVersionRepository(
        IHttpContextAccessor httpContextAccessor,
        ITenantConnectionFactory connectionFactory)
        : base(httpContextAccessor, connectionFactory)
    {
    }

    public async Task<FileVersion> GetByFileId(int fileId)
    {
        string sql = @"select * from dms.fileversion where fileid=@fileId";
        return await Connection.QueryFirstOrDefaultAsync<FileVersion>(sql, new { fileId });
    }
}
