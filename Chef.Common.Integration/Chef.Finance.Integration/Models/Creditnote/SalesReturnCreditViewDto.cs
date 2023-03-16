using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Finance.Integration.Models;

public class SalesReturnCreditViewDto
{
    public int ItemId { get; set; }
    public int LedgerAccountId { get; set; }
    public string LedgerAccountCode { get; set; }
    public string LedgerAccountName { get; set; }
    public decimal DebitAmount { get; set; }

    public decimal DebitAmountInBaseCurrency { get; set; }

    public decimal CreditAmount { get; set; }

    public decimal CreditAmountInBaseCurrency { get; set; }

    public int BranchId { get; set; }

    public int FinancialYearId { get; set; }
}
