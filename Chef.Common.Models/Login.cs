using Chef.Common.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Common.Models
{
   public class Login :Model
    {
        [EmailAddress]
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
