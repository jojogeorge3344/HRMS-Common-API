using Chef.Common.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Common.Models
{
    public class TimeZone : Model
    {
        [Required]
        public string TimeZoneId { get; set; }
        [Required]
        public string DisplayName { get; set; }
        [Required]
        public string BaseUtcOffset { get; set; }


    }
}
