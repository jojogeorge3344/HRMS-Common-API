using Chef.Common.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chef.Common.Models
{
   public class Permission:Model
    {
        
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
