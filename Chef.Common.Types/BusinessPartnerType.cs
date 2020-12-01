using System.ComponentModel;

namespace Chef.Common.Types
{
    public enum BusinessPartnerGroupType
    {
        [Description("Trade")]
        EBP = 1,

        [Description("Affiliate")]
        ABP,

        [Description("Employees")]
        HBP,

        [Description("Others")]
        OBP
    }
}