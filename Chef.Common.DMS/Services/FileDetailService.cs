namespace Chef.Common.DMS.Services;

public class FileDetailService : AsyncService<FileDetail>, IFileDetailService
{
    private readonly IFileDetailRepository fileDetailRepository;
    private readonly IFileMetaDataRepository fileMetaDataRepository;
    private readonly IFileVersionRepository fileVersionRepository;

    private readonly IStorageService storageService;
    private readonly StorageOptions storageOptions;
    private readonly ITenantSimpleUnitOfWork simpleUnitOfWork;

    public FileDetailService(
        IFileDetailRepository fileDetailRepository,
        IFileMetaDataRepository fileMetaDataRepository,
        IFileVersionRepository fileVersionRepository,
        IStorageService fileStorageService,
        IOptions<StorageOptions> options,
        ITenantSimpleUnitOfWork simpleUnitOfWork)
    {
        this.fileDetailRepository = fileDetailRepository;
        this.fileMetaDataRepository = fileMetaDataRepository;
        this.fileVersionRepository = fileVersionRepository;

        this.storageService = fileStorageService;
        this.storageOptions = options.Value;
        this.simpleUnitOfWork = simpleUnitOfWork;
    }

    public async Task<DocumentFile> GetFile(int id)
    {
        var fileDetail = await fileDetailRepository.GetAsync(id);
        var fileMetaData = await fileMetaDataRepository.GetAsync(id);

        var content = await storageService.Get(fileDetail.Path, fileDetail.Name);

        return new DocumentFile()
        {
            Name = fileDetail.Name,
            Type = fileDetail.Type,

            BranchId = fileMetaData.BranchId,
            CompanyId = fileMetaData.CompanyId,
            Feature = fileMetaData.Feature,
            Module = fileMetaData.Module,

            Content = content
        };
    }

    public async Task<IEnumerable<DocumentFile>> GetFiles(int[] ids)
    {
        var files = new List<DocumentFile>();

        //TODO change this logic to bulk get.
        foreach (int id in ids)
        {
            files.Add(await GetFile(id));
        }

        return files;
    }

    public async Task<byte[]> DownloadFile(int id)
    {
        var fileDetail = await fileDetailRepository.GetAsync(id);
        return await storageService.Get(fileDetail.Path, fileDetail.Name);
    }

    public async Task<int> Insert(DocumentFile documentFile)
    {
        return await SaveFile(documentFile);
    }

    public async Task<IList<int>> Insert(IEnumerable<DocumentFile> documentFiles)
    {
        IList<int> ids = new List<int>();

        //save this logic to bulk insert
        foreach (DocumentFile documentFile in documentFiles)
        {
            var id = await SaveFile(documentFile);
            ids.Add(id);
        }

        return ids;
    }

    public async Task<bool> Update(int id, byte[] content)
    {
        var fileDetail = await fileDetailRepository.GetAsync(id);
        var fileMetaData = await fileMetaDataRepository.GetAsync(id);

        //move to archive folder.
        var archivePath = GetArchivesFilePath(
            fileMetaData.CompanyId,
            fileMetaData.BranchId,
            fileMetaData.Module);

        //get the extension.
        var fileName = fileDetail.Name.Split(".");
        var archiveFileName = $"{fileName[0]}_{fileDetail.CurrentVersion}.{fileName[1]}";

        string sourceFile = Path.Combine(fileDetail.Path, fileDetail.Name);
        string archiveFile = Path.Combine(archivePath, archiveFileName);

        try
        {
            //move to archive folder.
            await storageService.Move(sourceFile, archivePath, archiveFileName);

            await this.fileVersionRepository.InsertAsync(new FileVersion()
            {
                FileId = fileDetail.Id,
                Version = fileDetail.CurrentVersion,
                Name = archiveFileName,
                Path = archivePath,
                Type = fileDetail.Type,
                Size = fileDetail.Size
            });

            await this.storageService.Save(content, fileDetail.Path, fileDetail.Name);
            var fileId = await this.fileDetailRepository.UpdateAsync(new FileDetail()
            {
                Id = id,
                Name = fileDetail.Name,
                Path = fileDetail.Path,
                Size = content.Length / 1024, //KB
                Type = fileDetail.Type,
                CurrentVersion = fileDetail.CurrentVersion + 1
            });
        }
        catch
        {
            //move back the file
            await storageService.Move(archiveFile, fileDetail.Path, fileDetail.Name);
            simpleUnitOfWork.Rollback();
            throw;
        }

        return true;
    }

    public Task<bool> DeleteFile(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteFiles(int[] ids)
    {
        throw new NotImplementedException();
    }

    private async Task<int> SaveFile(DocumentFile documentFile)
    {
        //get the path where the file to be stored.
        string filePath = GetFilePath(documentFile.CompanyId, documentFile.BranchId, documentFile.Module);
        string fileType = documentFile.Name.Split(".")[0] ?? string.Empty;
        int fileId = 0;

        //save file
        await storageService.Save(documentFile.Content, filePath, documentFile.Name);

        try
        {
            simpleUnitOfWork.BeginTransaction();

            fileId = await this.fileDetailRepository.InsertAsync(
                new FileDetail()
                {
                    Name = documentFile.Name,
                    Path = filePath,
                    Size = documentFile.Content.Length / 1024,
                    Type = fileType,
                    CurrentVersion = 1
                });

            await this.fileMetaDataRepository.InsertAsync(
                new FileMetaData()
                {
                    FileId = fileId,
                    CompanyId = documentFile.CompanyId,
                    BranchId = documentFile.BranchId,
                    Module = documentFile.Module,
                    Feature = documentFile.Feature,
                    RefNumber = documentFile.RefNumber
                });

            await this.fileVersionRepository.InsertAsync(
                new FileVersion()
                {
                    FileId = fileId,
                    Name = documentFile.Name,
                    Path = filePath,
                    Size = 0,
                    Type = fileType
                });

            simpleUnitOfWork.Commit();
        }
        catch
        {
            //TODO revisit this on multiple file scenario.
            await storageService.Delete(filePath, documentFile.Name);
            simpleUnitOfWork.Rollback();
            throw;
        }

        return fileId;
    }

    private async Task<DocumentFile> GetDocumentFile(int id)
    {
        var fileDetail = await fileDetailRepository.GetAsync(id);
        var fileMetaData = await fileMetaDataRepository.GetAsync(id);

        return new DocumentFile()
        {
            Name = fileDetail.Name,
            Type = fileDetail.Type,

            BranchId = fileMetaData.BranchId,
            CompanyId = fileMetaData.CompanyId,
            Feature = fileMetaData.Feature,
            Module = fileMetaData.Module,
        };
    }

    private string GetFilePath(int companyId, int branchId = 0, string module = "default")
    {
        string path = string.Format("{0}/{1}/{2}/{3}/{4}/{5}/",
            storageOptions.BasePath,
            companyId,
            branchId,
            module,
            DateTime.UtcNow.Year,
            DateTime.UtcNow.Month);

        return path;
    }

    private string GetArchivesFilePath(int companyId, int branchId = 0, string module = "default")
    {
        string path = string.Format("{0}/archives/{1}/{2}/{3}/{4}/{5}/",
            storageOptions.BasePath,
            companyId,
            branchId,
            module,
            DateTime.UtcNow.Year,
            DateTime.UtcNow.Month);

        return path;
    }
}
