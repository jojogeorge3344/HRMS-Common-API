using Chef.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chef.Common.Dtos
{
    public class TaxSetupEntryDTO : Tax
    {
        public string TaxJurisdiction { get; set; }
        public string ItemSegmentName { get; set; }
        public string itemclassname { get; set; }
        public bool AutoGenerateCode { get; set; } = true;
    }
}
