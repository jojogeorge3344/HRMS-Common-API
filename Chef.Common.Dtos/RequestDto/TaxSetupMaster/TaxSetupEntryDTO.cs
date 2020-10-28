using Chef.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chef.Common.Dtos
{
    public class TaxSetupEntryDTO : TaxSetup
    {
        public string TaxJurisdiction { get; set; }
        public string ItemSegmentName { get; set; }
        public string itemclassname { get; set; }
        public bool AutoGenerateCode { get; set; } = true;
    }
}
