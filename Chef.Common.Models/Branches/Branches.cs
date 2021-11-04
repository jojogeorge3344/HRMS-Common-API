using Chef.Common.Core;

namespace Chef.Common.Models
{
    public class Branches: Model
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public int CompanyId{ get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
    }
}
