using System;
using System.ComponentModel.DataAnnotations;
using Chef.Common.Core;

namespace Chef.Common.Models
{
    public class NumberingSchema : Model
    {
        
        [Required]
        public string BpType { get; set; }
        public string BpCode { get; set; }
        
        public int CompanyPrefix { get; set; }
       
        public int BranchPrefix { get; set; }
        
        public int YearCode { get; set; }
        [Required]
        public int SerialNumber { get; set; }
        public int FreeNumber { get; set; }

        public bool IsDefault { get; set; }
       
    }
}
