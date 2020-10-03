using Chef.Common.Core;
using Chef.Common.Models.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chef.Common.Models
{
   public class TaxSetup : Model
    {
        public TaxType TaxType { get; set; }
        public string TaxCode { get; set; }
        public string TaxDescription { get; set; }
        public int TaxJurisdictionId { get; set; }
        public int ItemSegmentId { get; set; }
        public int ItemClassId { get; set; }
        public int MainTax { get; set; }
        public int TaxPercentage { get; set; }

       
        
        
       
    }
}
