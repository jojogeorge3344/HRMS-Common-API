﻿using Chef.Common.Core;
using Chef.Common.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chef.Common.Models
{
    public class TaxJurisdictionCodeFormat : Model
    {
        [Required]
        public CodeType CodeType { get; set; }

        [Required]
        public string CodeFormat { get; set; }

        public int? MaxCounter { get; set; }
    }
}
