using Chef.Common.Core;
using Chef.HRMS.Integration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Common.Models;

public class HRMSPaygroupConfiguration:Model
{
    public int PayGroupID { get; set; }
    public string PayGroupCode { get; set; }
    public string PayGroupName { get; set; }
    public int JournalBookId { get; set; }
    public string JournalBookCode { get; set; }
    public string JournalBookName { get; set; }

    [Write(false)]
    [Skip(true)]
    [SqlKata.Ignore]
    public List<PayRollComponentViewModel> dropDownDetails { get; set; }

    [Write(false)]
    [Skip(true)]
    [SqlKata.Ignore]
    public List<HRMSPayGroupPayRollComoponentDetails> hRMSPayGroupPayRollComoponentDetails { get; set; }


}
