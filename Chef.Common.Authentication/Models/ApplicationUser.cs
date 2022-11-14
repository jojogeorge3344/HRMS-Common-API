using Microsoft.AspNetCore.Identity;

namespace Chef.Common.Authentication.Models;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string TimeZone { get; set; }
    public bool IsActive { get; set; }
    public int DefaultBranchId { get; set; }
}