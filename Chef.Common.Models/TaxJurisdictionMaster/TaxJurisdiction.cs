using Chef.Common.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace Chef.Common.Models
{
   public class TaxJurisdiction : Model
    {
        [Required(AllowEmptyStrings = true)]
        [Unique(true)]
        public string TaxJurisdictionCode { get; set; } = string.Empty;
        [Required]
        [Unique(true)]
        public string TaxJurisdictionName { get; set; }
    }
}
