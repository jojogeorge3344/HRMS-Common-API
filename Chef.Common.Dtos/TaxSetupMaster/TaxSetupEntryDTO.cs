using Chef.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chef.Common.Dtos
{
    public class TaxSetupEntryDTO : TaxSetup
    {
        public bool AutoGenerateCode { get; set; } = true;
    }
}
