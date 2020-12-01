using Chef.Common.Core;
using System.ComponentModel.DataAnnotations;

namespace Chef.Common.Models
{
    public abstract class BankAccount : Model
    {
        [Required]
        [StringLength(32)]
        public string AccountNumber { get; set; }

        [Required]
        [StringLength(126)]
        public string AccountName { get; set; }

        [Required]
        [StringLength(64)]
        public string BankName { get; set; }

        [Required]
        [StringLength(16)]
        public string IFSCCode { get; set; }

        [Required]
        [StringLength(64)]
        public string BranchName { get; set; }
    }
}
