namespace Chef.Common.DMS.Repositories;

public interface IFileMetaDataRepository : IGenericRepository<FileMetaData>
{
    Task<FileMetaData> GetByFileId(int fileId);
}

