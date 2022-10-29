using System;

namespace Chef.Common.Models
{
	public class FinancialYearPeriodDto
	{
        public int Id { get; set; }
        public int FinancialYearId { get; set; }
        public string Period { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Status { get; set; }
    }
}

