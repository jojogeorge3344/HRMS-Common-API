using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Chef.Common.Core;

namespace Chef.Common.Models
{
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

        //TO DO require clarification
        public string Frequency { get; set; }

        //TO DO require clarification
        public string PeriodFormat { get; set; }

        [Write(false)]
        [Skip(true)]
        public List<FinancialYearPeriod> FinancialYearPeriod { get; set; }
    }
}