using Chef.Common.Core;
using Chef.Common.Types;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Chef.Common.Models
{
   public class Role:Model
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public bool IsRoleActive { get; set; }
        [Write(false)]
        [Skip(true)]
        public List<RoleNode> RoleNodeList { get; set; }
       
    }
}
