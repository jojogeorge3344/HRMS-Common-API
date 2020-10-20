using Chef.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chef.Common.Dtos
{
    public class TaxJurisdictionEntryDTO : TaxJurisdiction
    {
        public bool AutoGenerateCode { get; set; } = true;
        //public string Host { get; set; }
    }
}
