﻿using System.ComponentModel.DataAnnotations;
using Chef.Common.Core;

namespace Chef.Common.Models
{
    public class ItemControlAccountMaster : Model
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string CategoryCode { get; set; }

        [Required]
        public string CategoryName { get; set; }
    }
}
