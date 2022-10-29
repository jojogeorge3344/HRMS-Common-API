using System.Collections.Generic;

namespace Chef.Common.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string TimeZone { get; set; }
        public bool IsActive { get; set; }
        public int DefaultBranchId { get; set; }
        public IList<RoleDto> Roles { get; set; }
        public IList<BranchDto> Branches { get; set; }
    }
}