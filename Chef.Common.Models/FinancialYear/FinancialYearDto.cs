using System;
using System.Collections.Generic;

namespace Chef.Common.Models;

public class FinancialYearDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    //TODO require clarification
    public string Frequency { get; set; }
    public string PeriodFormat { get; set; }

    public IList<FinancialYearPeriodDto> Periods {get; set;}
}
