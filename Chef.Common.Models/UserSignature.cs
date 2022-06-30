using Chef.Common.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Common.Models
{
    public class UserSignature:Model
    {
        [ForeignKey("Common.User")]
        public int UserId { get; set; }

        public string UserEmail { get; set; }

        public string  FileName { get; set; }

        public byte[] Signature { get; set; }
    }
}
