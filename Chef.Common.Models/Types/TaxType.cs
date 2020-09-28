using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Chef.Common.Models.Types
{
    public enum TaxType
    {
        [Description("Purchase")]
        Purchase = 1,

        [Description("Sales")]
        Sales=2
    }
}
