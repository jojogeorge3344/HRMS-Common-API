using System;
using System.Collections.Generic;
using System.Text;

namespace Chef.Common.Types
{
    public enum PaymentOverDueType
    {
        DueInvoicesOnly = 1,
        OverDueDays = 2,
        AllInvoices = 3
    }
}
