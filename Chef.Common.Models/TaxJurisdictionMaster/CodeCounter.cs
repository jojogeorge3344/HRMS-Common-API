using Chef.Common.Core;
using System.ComponentModel.DataAnnotations;

namespace Chef.Common.Models
{
    public class CodeCounter : Model
    { 
        [Required]
        public string CodePrefix { get; set; }  
 
        public int Counter { get; set; }
        
        public int DeleteCounter { get; set; }
    }
}