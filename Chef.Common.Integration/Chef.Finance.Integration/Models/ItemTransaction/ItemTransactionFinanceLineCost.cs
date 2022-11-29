using Chef.Common.Core;
using System.ComponentModel.DataAnnotations;

namespace Chef.Finance.Integration.Models;
public class ItemTransactionFinanceLineCost  : Model
    {
    
        public int ItemTransactionFinanceId { get; set; }

        public int ItemCategory { get; set; }

     
        public int ItemType { get; set; }

    
        public int ItemSegmentId { get; set; }

      
        public int ItemFamilyId { get; set; }

      
        public int ItemClassId { get; set; }

       
        public int ItemCommodityId { get; set; }

    
        public int ItemId { get; set; }

        [StringLength(50)]
        public string Currency { get; set; }

        public decimal ExRate { get; set; }

        public decimal TransAmount { get; set; }

        public decimal HmAmount { get; set; }

    }

