using System.ComponentModel;

namespace Chef.Common.Models.Types
{
    public enum TaxType
    {
        [Description("Purchase")]
        Purchase = 1,

        [Description("Sales")]
        Sales = 2
    }
}
