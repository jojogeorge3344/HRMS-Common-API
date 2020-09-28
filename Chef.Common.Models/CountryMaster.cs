using System.ComponentModel.DataAnnotations;
using Chef.Common.Core;

namespace Chef.Common.Models
{
    public class CountryMaster : Model
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public string DialCode { get; set; }

        public string CurrencyName { get; set; }

        public string CurrencyCode { get; set; }

        public string CurrencySymbol { get; set; }
    }
}
