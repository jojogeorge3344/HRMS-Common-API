using Chef.Common.Models;
using Refit;

namespace Chef.Common.Api;

public interface IDmsApi
{
    [Get("/GetFile/{id}")]
    Task<DocumentFile> GetFile(int id);

    [Get("/GetFiles/{ids}")]
    Task<IEnumerable<DocumentFile>> GetFiles(int[] ids);

    [Get("/DownloadFile/{id}")]
    Task<byte[]> DownloadFile(int id);

    [Get("/SaveFile")]
    Task<int> SaveFile([Body] DocumentFile documentFile);

    [Get("/SaveFiles")]
    Task<IList<int>> SaveFiles([Body] IEnumerable<DocumentFile> documentFiles);

    Task<bool> Update(int fileId, byte[] content);

    Task<bool> DeleteFile(int id);
    Task<bool> DeleteFiles(int[] ids);
    
    Task<byte> GetFile(Models.DocumentFile ms, string token);
}
