using Chef.Common.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Common.Models
{
    public class StateMaster : Model
    {
        [Required]
        [StringLength(126)]
        public string Name { get; set; }

        [Required]
        [ForeignKey("Common.Country")]
        public int CountryId { get; set; }
    }
}
