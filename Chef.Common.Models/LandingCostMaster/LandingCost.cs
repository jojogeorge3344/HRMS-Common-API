using Chef.Common.Core;
using System.ComponentModel.DataAnnotations;

namespace Chef.Trading.Models
{
    public class LandingCost : Model
    {
        [Required]
        [Unique(true)]
        [Code]
        public string LandingCostCode { get; set; }

        [Required]
        [Unique(true)]
        public string LandingCostName { get; set; }

        public string CostGroupCode { get; set; }

        public string CostGroupName { get; set; }

        public int CostPriceComponentId { get; set; }

        public string CostPriceComponentCode { get; set; }

        public string CostPriceComponentName { get; set; }
    }
}
