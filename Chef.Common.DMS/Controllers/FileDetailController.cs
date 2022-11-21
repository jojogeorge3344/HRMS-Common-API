namespace Chef.Common.DMS.Controllers;

[Authorize]
[ApiController]
[Route("api/console/[controller]/[action]")]
public class FileDetailController : ControllerBase
{
    private readonly IFileDetailService fileDetailService;

    public FileDetailController(IFileDetailService fileDetailService)
    {
        this.fileDetailService = fileDetailService;
    }

    [HttpGet]
    [Route("{id:int}", Name = nameof(GetFile))]
    public async Task<ActionResult<DocumentFile>> GetFile(int id)
    {
        return Ok(await fileDetailService.GetFile(id));
    }

    [HttpPost(nameof(SaveFile))]
    public async Task<ActionResult<int>> SaveFile(DocumentFile documentFile)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(await fileDetailService.Insert(documentFile));
    }

    [HttpPost(nameof(SaveFiles))]
    public async Task<ActionResult<int>> SaveFiles(IList<DocumentFile> documentFiles)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(await fileDetailService.Insert(documentFiles));
    }

    [HttpPost(nameof(UpdateFile))]
    public async Task<ActionResult<bool>> UpdateFile(int fileId, Byte[] content)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(await this.fileDetailService.Update(fileId, content));
    }

    [HttpDelete]
    [Route("{fileId:int}", Name = nameof(DeleteFile))]
    public async Task<ActionResult<bool>> DeleteFile(int fileId)
    {
        return Ok(await fileDetailService.DeleteFile(fileId));
    }

    [HttpDelete]
    [Route("{fileIds}", Name = nameof(DeleteFiles))]
    public async Task<ActionResult<bool>> DeleteFiles(int[] fileIds)
    {
        return Ok(await fileDetailService.DeleteFiles(fileIds));
    }
}
