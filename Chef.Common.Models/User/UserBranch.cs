using Chef.Common.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chef.Common.Models
{
   public class UserBranch: Model
    {
        [ForeignKey("User")]
        public int UserId { get; set; }

        public string UserName { get; set; }

        [ForeignKey("Branches")]
        public int BranchId { get; set; }
    }
}
