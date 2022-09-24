﻿using System.ComponentModel.DataAnnotations.Schema;
using Chef.Common.Core;

namespace Chef.Common.Models
{
    public class UserBranch : Model
    {
        [ForeignKey("User")]
        public int UserId { get; set; }

        public string UserName { get; set; }

        [ForeignKey("Branch")]
        public int BranchId { get; set; }
    }
}
