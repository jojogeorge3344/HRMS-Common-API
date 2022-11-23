using Chef.Common.Core;

namespace Chef.Common.Models
{
    public class BranchViewModel : Model
    {
        public int Id { get; set; }
        public int BranchId { get; set; }

        public string BranchName { get; set; }

        public string BranchCode { get; set; }
    }
}