namespace Chef.Common.Authentication.Models;

public class UserBranchDto : Model
{
    public string UserName { get; set; }
    public int BranchId { get; set; }
    public string BranchName { get; set; }
    public string BranchCode { get; set; }
    public bool IsDefault { get; set; }
}
