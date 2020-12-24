using Chef.Common.Core;

namespace Chef.Common.Models
{
    public class CompanyViewModel : Model
    {
        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        public string CompanyCode { get; set; }
    }
}
