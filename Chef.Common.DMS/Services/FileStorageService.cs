namespace Chef.Common.DMS.Services;

public class FileStorageServices : IStorageService
{
    public Task Delete(string filePath, string fileName)
    {
        string file = System.IO.Path.Combine(filePath, fileName);
        return Task.Factory.StartNew(() => File.Delete(file));
    }

    public async Task<byte[]> Get(string filePath, string fileName)
    {
        string file = Path.Combine(filePath, fileName);
        return await File.ReadAllBytesAsync(file);
    }

    public Task Move(
        string sourceFilePath,
        string destinationFolder,
        string destinationFileName)
    {
        if (!Directory.Exists(destinationFolder))
        {
            Directory.CreateDirectory(destinationFolder);
        }

        var destinationFile = Path.Combine(destinationFolder, destinationFileName);

        return Task.Factory.StartNew(() => File.Move(sourceFilePath, destinationFile, true));
    }

    public async Task<bool> Save(byte[] content, string filePath, string fileName)
    {
        if (!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
        }

        string file = Path.Combine(filePath, fileName);
        await File.WriteAllBytesAsync(file, content);

        return true;
    }
}

