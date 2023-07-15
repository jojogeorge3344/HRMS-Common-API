using Chef.Common.Core;
using SqlKata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Chef.Common.Models;

public class FinancialYear : Model
{
    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    [Required]
    [StringLength(12)]
    public string Code { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    //TODO require clarification
    public string Frequency { get; set; }

    //TODO require clarification
    public string PeriodFormat { get; set; }

    [Write(false)]
    [Skip(true)]
    [SqlKata.Ignore]
    public List<FinancialYearPeriod> Periods { get; set; }
}