using System.ComponentModel;

namespace Chef.Common.Models
{
    public enum BusinessType
    {
        [Description("Public Sector")]
        PublicSector = 1,

        [Description("Private Limited")]
        PrivateLimited = 2,

        [Description("Limited Liability")]
        LimitedLiability = 3,

        [Description("Public Limited")]
        PublicLimited = 4,

        [Description("Proprietorship")]
        Proprietorship = 5,

        [Description("Partnership Firm")]
        PartnershipFirm = 6,

        [Description("Foreign Company")]
        ForeignCompany = 7,

        [Description("Society/Club/Trust/NGO/AOP")]
        Trust = 8,

        [Description("Others")]
        Others = 9
    }
}
