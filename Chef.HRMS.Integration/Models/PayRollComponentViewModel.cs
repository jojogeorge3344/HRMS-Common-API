using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.HRMS.Integration;

public class PayRollComponentViewModel
{
    public int HrmsPaygroupConfigurationId { get; set; }
    public int ComponentId { get; set; }
    public string ComponentCode { get; set; }
    public string ComponentName { get; set; }
    public int ComponentTypeId { get; set; }
    public string ComponentTypeName { get; set; }
    public int DebitLedgerAccountId { get; set; }
    public string DebitLedgerAccountCode { get; set; }
    public string DebitLedgerAccountName { get; set; }
    public int CreditLedgerAccountId { get; set; }
    public string CreditLedgerAccountCode { get; set; }
    public string CreditLedgerAccountName { get; set; }
}
