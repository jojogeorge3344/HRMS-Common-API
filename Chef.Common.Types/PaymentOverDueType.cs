using System.ComponentModel;

namespace Chef.Common.Types
{
    public enum PaymentOverDueType
    {
        [Description("Due Invoices Only")]
        DueInvoicesOnly = 1,

        [Description("Over Due Days")]
        OverDueDays =2,

        [Description("All Invoices")]
        AllInvoices =3,
    }
}
