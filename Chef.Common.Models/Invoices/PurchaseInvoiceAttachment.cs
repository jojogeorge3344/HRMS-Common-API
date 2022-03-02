﻿using Chef.Common.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Finance.Models
{
    public class PurchaseInvoiceAttachment : Model
    {
        [Required]
        [ForeignKey("PurchaseInvoiceOtherDetail")]
        public int PurchaseInvoiceId { get; set; }

        public string FileName { get; set; }

        public int FileId { get; set; }
    }
}
