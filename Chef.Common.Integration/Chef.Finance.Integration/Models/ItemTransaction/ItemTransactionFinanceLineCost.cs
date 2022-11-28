using Chef.Common.Core;
using System.ComponentModel.DataAnnotations;

namespace Chef.Finance.Integration.Models;
public class ItemTransactionFinanceLineCost  : Model
    {
        [Required]
        public int ItemTransactionFinanceId { get; set; }

        public int ItemCategory { get; set; }

        [Required]
        public int ItemType { get; set; }

        [Required]
        public int ItemSegmentId { get; set; }

        [Required]
        public int ItemFamilyId { get; set; }

        [Required]
        public int ItemClassId { get; set; }

        [Required]
        public int ItemCommodityId { get; set; }

        [Required]
        public int ItemId { get; set; }

        [StringLength(50)]
        public string Currency { get; set; }

        public decimal ExRate { get; set; }

        public decimal TransAmount { get; set; }

        public decimal HmAmount { get; set; }

    }

