using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Common.Models
{
  public  class NodeBasedRoleView
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int RoleId { get; set; }
        public int RoleNodeId { get; set; }
    }
}
