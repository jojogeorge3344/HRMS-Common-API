using Chef.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Common.Models
{
    public class PurchaseInvoiceGRNDetails : Model
    {
        public int PoId { get; set; }
        public string GRNNumber { get; set; }
        public DateTime? GRNDate { get; set; }
        public decimal GRNAmount { get; set; }
        public decimal GRNTaxAmount { get; set; }
        public decimal GRNTotalAmount { get; set; }
        public decimal GRNTotalAmountInBaseCurrency { get; set; }
        public decimal GRNExchangeRate { get; set; }
        public DateTime? GRNExchangeDate { get; set; }
    }
}
