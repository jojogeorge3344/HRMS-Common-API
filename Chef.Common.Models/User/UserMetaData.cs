using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Common.Models
{
    public class UserMetaData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string NodeName { get; set; }
        public string PermissionName { get; set; }
        public int UserId { get; set; }

        //public string Name { get; set; }
        //public string Code { get; set; }
        //public string NodeName { get; set; }

    }
}
