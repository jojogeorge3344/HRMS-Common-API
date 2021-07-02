using Chef.Common.Core;
using Chef.Common.Types;
using System.ComponentModel.DataAnnotations;


namespace Chef.Common.Models
{
   public class Role:Model
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
