using Chef.Common.Core;
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

        public int? ItemTypeId { get; set; }

        public string ItemTypeName { get; set; }

        public int? CostPriceComponentId { get; set; }

        public string CostPriceComponentName { get; set; }

        public int? ItemSegmentId { get; set; }

        public string ItemSegmentName { get; set; }

        public int? ItemClassId { get; set; }

        public string ItemClassName { get; set; }

        public int? ItemFamilyId { get; set; }

        public string ItemFamilyName { get; set; }

        public int? ItemCommondityId { get; set; }

        public string ItemCommondityName { get; set; }

        [Write(false)]
        [Skip(true)]
        public List<IntegrationControlAccount> IntegrationControlAccounts { get; set; }
    }
}
