using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Common.Models.Types
{
    public enum TaxScope
    {
        [Description("Sales")]
        Sales = 1,

        [Description("Purchase")]
        Purchase = 2,

        [Description("None")]
        None = 3
    }
}
