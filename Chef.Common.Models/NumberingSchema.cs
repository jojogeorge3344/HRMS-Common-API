using System.ComponentModel.DataAnnotations;
using Chef.Common.Core;

namespace Chef.Common.Models
{
    public class NumberingSchema : Model
    {
        public int FreeNumber { get; set; }

        //TODO: DELETE FOLLOWING FIELDS AND DB.
        public int CompanyPrefix { get; set; }
        public int BranchPrefix { get; set; }
        public bool IsDefault { get; set; }
        public int YearCode { get; set; }
        public string BpCode { get; set; }
        [Required]
        public string BpType { get; set; }
        [Required]
        public int SerialNumber { get; set; }
    }
}
