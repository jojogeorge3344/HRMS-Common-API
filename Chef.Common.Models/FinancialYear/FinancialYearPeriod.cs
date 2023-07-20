using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Chef.Common.Core;
using Chef.Common.Types;

namespace Chef.Common.Models;

public class FinancialYearPeriod : Model
{
    [ForeignKey("FinancialYear")]
    [Required]
    public int FinancialYearId { get; set; }

    public string Period { get; set; }

    [Required]
    public DateTime FromDate { get; set; }

    [Required]
    public DateTime ToDate { get; set; }

    public FinancialYearStatusType Status { get; set; }
}
