using Chef.Common.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chef.Common.Models
{
    public class SubTax : Model
    {
        public string SubTaxName { get; set; }
        public int SubTaxPercentage { get; set; }
        public string TaxBase { get; set; }
    }
}
