using Refit;

namespace Chef.Common.Api;

public interface IManufacturingApi
{
    [Put("/BasicMaterialIssue/UpdateStatus/{materialIssueId}/{status}/{remark}")]
    Task<int> UpdateMaterialIssueStatus(int materialIssueId, int status, string remark);
}
