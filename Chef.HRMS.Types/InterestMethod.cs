using System.ComponentModel;

namespace Chef.HRMS.Types
{
    public enum InterestMethod
    {
        [Description("Reduction Rate")]
        ReductionRate = 1,
        [Description("Flat Rate")]
        FlatRate,
    }
}
