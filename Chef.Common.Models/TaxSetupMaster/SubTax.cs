using Chef.Common.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chef.Common.Models
{
    public class SubTax : Model
    {
        [ForeignKeyId(typeof(Tax))]
        [Required]
        public int TaxId { get; set; }
        [ForeignKeyCode(typeof(Tax))]
        [Required]
       
        public string TaxCode { get; set; }

        [Required]
        [Unique(true)]
        public string SubTaxName { get; set; }
        [Required]
        public float SubTaxPercent { get; set; }
        [Required]
        public string TaxBase { get; set; }
    }
}
