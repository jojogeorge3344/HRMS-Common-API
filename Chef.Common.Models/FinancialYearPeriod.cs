using System;
using System.ComponentModel.DataAnnotations;
using Chef.Common.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chef.Common.Models
{
  public  class FinancialYearPeriod : Model
    {
        [ForeignKey("FinancialYear")]
        [Required]
        public int FinancialYearId { get; set; }
        public string Period { get; set; }
        [Required]
        public DateTime FromDate { get; set; }

        [Required]
        public DateTime ToDate { get; set; }
        public string Status { get; set; }
    }
}
