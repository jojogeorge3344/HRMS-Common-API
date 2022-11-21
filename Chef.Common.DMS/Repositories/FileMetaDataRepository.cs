namespace Chef.Common.DMS.Repositories;

public class FileMetaDataRepository : TenantRepository<FileMetaData>, IFileMetaDataRepository
{
    public FileMetaDataRepository(
        IHttpContextAccessor httpContextAccessor,
        ITenantConnectionFactory connectionFactory)
        : base(httpContextAccessor, connectionFactory)
    {
    }
}
