namespace Chef.Common.DMS.Services;

public interface IFileDetailService : IAsyncService<FileDetail>
{
    Task<DocumentFile> GetFile(int id);
    Task<IEnumerable<DocumentFile>> GetFiles(int[] ids);

    Task<byte[]> DownloadFile(int id);

    Task<int> Insert(DocumentFile documentFile);
    Task<IList<int>> Insert(IEnumerable<DocumentFile> documentFiles);

    Task<bool> Update(int fileId, byte[] content);

    Task<bool> DeleteFile(int id);
    Task<bool> DeleteFiles(int[] ids);
}
