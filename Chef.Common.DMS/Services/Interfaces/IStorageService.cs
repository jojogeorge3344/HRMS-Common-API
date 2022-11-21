namespace Chef.Common.DMS.Services;

public interface IStorageService : IBaseService
{
    Task<byte[]> Get(string filePath, string fileName);
    Task<bool> Save(byte[] content, string filePath, string fileName);
    Task Delete(string filePath, string fileName);
    Task Move(string sourceFilePath, string destinationFolder, string destinationFileName);
}
