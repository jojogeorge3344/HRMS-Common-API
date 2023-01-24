
using Chef.Common.Core;
using Chef.Common.Types;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Chef.Common.Models
{
    public class FinanceIntegrationConfiguration : Model
    {
        [Required]
        public int ItemCategoryId { get; set; }

        [Required]
        public string ItemCategoryName { get; set; }

        public int? ItemTypeId { get; set; }=0;

        public string ItemTypeName { get; set; }

        public int? CostPriceComponentId { get; set; } = 0;

        public string CostPriceComponentName { get; set; }

        public int? ItemSegmentId { get; set; } = 0;

        public string ItemSegmentName { get; set; }

        public int? ItemClassId { get; set; } = 0;

        public string ItemClassName { get; set; }

        public int? ItemFamilyId { get; set; } = 0;

        public string ItemFamilyName { get; set; }

        public int? ItemCommondityId { get; set; } = 0;

        public string ItemCommondityName { get; set; }

        [Required]
        public int TransactionOriginId { get; set; }

        [Required]
        public string TransactionOriginName { get; set; }

        [Required]
        public int TransactionTypeId { get; set; }

        [Required]
        public string TransactionTypeName { get; set; }

        public int PurchaseOrderGroupId { get; set; }

        public string PurchaseOrderGroupName { get; set; }

        [Write(false)]
        [Skip(true)]
        [SqlKata.Ignore]
        public List<IntegrationControlAccount> IntegrationControlAccounts { get; set; }
    }
}
