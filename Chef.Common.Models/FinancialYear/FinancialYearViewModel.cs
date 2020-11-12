﻿using Chef.Common.Core;
using System;

namespace Chef.Common.Models
{
    public class FinancialYearViewModel : Model
    {
        public string Name { get; set; }

        public string Code { get; set; }
        
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        //TO DO require clarification
        public string Frequency { get; set; }

        public string PeriodFormat { get; set; }
        
        public int FinancialYearId { get; set; }
        
        public string Period { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }
        
        public string Status { get; set; }
    }
}
