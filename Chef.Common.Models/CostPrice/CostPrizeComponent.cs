using Chef.Common.Core;

namespace Chef.Common.Models.Master
{
    public class CostPrizeComponent : Model
    {
        public int CpcId { get; set; }
        public string CpcCode { get; set; }
        public string CpcName { get; set; }
    }
}
