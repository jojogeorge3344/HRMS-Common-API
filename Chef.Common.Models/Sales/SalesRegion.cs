using Chef.Common.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Common.Models
{
    public class SalesRegion : TransactionModel
    {
        [Required]
        [StringLength(100)]
        public string SalesRegionCode { get; set; }

        [Required]
        [StringLength(1000)]
        public string SalesRegions { get; set; }

        [Required]
        public int CountryId { get; set; }

        [Required]
        public bool IsAssigned { get; set; } = true;

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public int RegionId { get; set; }
    }

}
