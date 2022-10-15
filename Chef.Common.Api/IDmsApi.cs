using Chef.Common.Models;
using Refit;

namespace Chef.Common.Api
{
    public interface IDmsApi
    {
        //TODO - verify the method name.
        [Get("/Role/GetAllRoles")]
        Task<string> GetAll(string Url);

        [Get("/FileUpload/UpdateFile")]
        Task<string> UpdateFileStream(FileSaving ms, string token);
    }
}
