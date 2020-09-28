using Chef.Common.Core;
using Chef.Common.Models.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chef.Common.Models
{
    public class TaxSetupCodeCounter : Model
    {
        [Required]
        public TaxType TaxType { get; }
        [Required]
        public int Counter { get; set; }
        public int DeleteCounter { get; set; }
    }
}
