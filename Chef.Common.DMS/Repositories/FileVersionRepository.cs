namespace Chef.Common.DMS.Repositories;

public class FileVersionRepository : TenantRepository<FileVersion>, IFileVersionRepository
{
    public FileVersionRepository(
        IHttpContextAccessor httpContextAccessor,
        ITenantConnectionFactory connectionFactory)
        : base(httpContextAccessor, connectionFactory)
    {
    }
}
