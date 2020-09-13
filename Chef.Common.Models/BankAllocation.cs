using System.ComponentModel.DataAnnotations;
using Chef.Common.Core;
using Chef.Common.Types;

namespace Chef.Common.Models
{
    public class BankAllocation: Model
    {
        public int CompanyId { get; set; }

        [Required]       
        public string CompanyCode { get; set; }

        public int BankId { get; set; }

        [Required]        
        public string BankName { get; set; }

        public int BranchId { get; set; }

        [Required]       
        public string BranchName { get; set; }

        public int CurrencyId { get; set; }

        [Required]
        public string CurrencyCode { get; set; }

        [Required]
        public string IBAN { get; set; }

        [Required]
        [StringLength(36)]
        public string AccountNumber { get; set; }

        [Required]
        [StringLength(126)]
        public string AccountName { get; set; }

        [Required]
        public BankAllocationAccountType AccountType { get; set; }

        [Required]
        public bool IsdefaultBank { get; set; }
    }
}
