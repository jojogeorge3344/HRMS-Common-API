using Chef.Common.Core;
using Chef.Common.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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
