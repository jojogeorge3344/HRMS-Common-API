using System.Collections.Generic;
using Chef.Common.Core;

namespace Chef.Common.Models
{
    public class Role : Model
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public bool IsRoleActive { get; set; } = false;

        [Write(false)]
        [Skip(true)]
        public List<RoleNode> RoleNodeList { get; set; }
    }
}
