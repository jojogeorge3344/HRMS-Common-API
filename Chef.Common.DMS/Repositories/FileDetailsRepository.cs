namespace Chef.Common.DMS.Repositories;

public class FileDetailRepository : TenantRepository<FileDetail>, IFileDetailRepository
{
    public FileDetailRepository(
        IHttpContextAccessor httpContextAccessor,
        ITenantConnectionFactory connectionFactory) :
        base(httpContextAccessor, connectionFactory)
    {
    }
}
