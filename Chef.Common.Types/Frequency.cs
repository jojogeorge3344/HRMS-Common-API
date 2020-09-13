using System.ComponentModel;

namespace Chef.Common.Types
{
    public enum Frequency
    {
        [Description("Cyclical")]
        Cyclical = 1,

        [Description("Month End")]
        MonthEnd
    }
}
