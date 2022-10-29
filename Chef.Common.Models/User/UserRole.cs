using System.ComponentModel.DataAnnotations.Schema;
using Chef.Common.Core;

namespace Chef.Common.Models
{
    public class UserRole : Model
    {
        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }
    }
}