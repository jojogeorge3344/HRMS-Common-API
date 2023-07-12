using System;

namespace Chef.Common.Models;

public class ApprovalMangementApproverViewModel
{
    public int Level { get; set; }

    public string Name { get; set; }

    public string Remarks { get; set; }

    public int Status { get; set; }

    public DateTime Approveddate { get; set; }

    public DateTime RejectedDate { get; set; }
}