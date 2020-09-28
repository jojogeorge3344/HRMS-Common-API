using System.ComponentModel;

namespace Chef.Common.Types
{
    public enum BusinessPartnerGroupType
    {
        [Description("External")]
        EBP = 1,

        [Description("Internal")]
        IBP,

        [Description("Affiliate")]
        ABP,

        [Description("Employees")]
        HBP,

        [Description("Others")]
        OBP
    }
}