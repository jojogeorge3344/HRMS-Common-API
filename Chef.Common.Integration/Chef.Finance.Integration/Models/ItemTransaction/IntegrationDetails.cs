using Chef.Common.Core;

namespace Chef.Finance.Integration.Models;
public class IntegrationDetails : TransactionModel
{
    public int integrationheaderid { get; set; }
    public int linenumber { get; set; }
    public int ledgeraccountid { get; set; }
    public string ledgeraccountcode { get; set; }
    public string ledgeraccountname { get; set; }
    public decimal debitamount { get; set; }
    public decimal debitamountinbasecurrency { get; set; }
    public decimal creditamount { get; set; }
    public decimal creditamountinbasecurrency { get; set; }
    public bool isdimensionallocation { get; set; }
    public string narration { get; set; }
    public int ItemTransactionFinanceId { get; set; }

    [Write(false)]
    [Skip(true)]
    [Ignore]
    public int ItemTransactionFinanceLineCostId { get; set; }

    [Write(false)]
    [Skip(true)]
    [Ignore]

    public string DocumentNumber { get; set; }


    [Write(false)]
    [Skip(true)]
    [Ignore]
    public DateTime TransactionDate { get; set; }
}
