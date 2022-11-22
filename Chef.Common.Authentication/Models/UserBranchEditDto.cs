using Chef.Common.Models;

namespace Chef.Common.Authentication.Models;

public class UserBranchEditDto : Branch
{
    public bool IsActive { get; set; }
    public bool IsDefault { get; set; }
}
