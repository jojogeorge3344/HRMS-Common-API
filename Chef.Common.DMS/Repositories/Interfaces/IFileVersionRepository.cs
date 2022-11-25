using Chef.DMS.Models;

namespace Chef.Common.DMS.Repositories;

public interface IFileVersionRepository : IGenericRepository<FileVersion>
{
    Task<FileVersion> GetByFileId(int fileId);
}