using Chef.Common.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chef.Common.Models
{
   public class TaxSetup : Model
    {
        public int TaxType { get; set; }
        public string TaxCode { get; set; }
        public int MainTaxId { get; set; }
        public int TaxPercentage { get; set; }

        public int ItemSegmentId { get; set; }
        public int ItemClassId { get; set; }
        public int TaxJurisdictionId { get; set; }
        public string TaxDescription { get; set; }
    }
}
