using Chef.Common.Core;

namespace Chef.Common.Models
{
	public class BranchViewModel : Model
	{

		public int BranchId { get; set; }

		public string BranchName { get; set; }

		public string BranchCode { get; set; }
        public string CompanyCode { get; set; }
        
    }
}
