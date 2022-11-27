using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Finance.Integration.Models;

    public class SalesReturnCreditItemTaxDto
    {
        public int SalesReturnItemTaxId { get; set; }
        public int SalesReturnId { get; set; }
        public int SalesReturnItemId { get; set; }
        public int SalesReturnCreditItemId { get; set; }
        public int ItemId { get; set; }
        public bool ItemTaxFlg { get; set; }
        public int SalesReturnAdditionalCostId { get; set; }
        public decimal GrossAmount { get; set; }      
        public int TaxId { get; set; }
        public int SurchargeId { get; set; }
        public int SurchargeType { get; set; }
        public decimal TaxPer { get; set; }
        public decimal TaxAmount { get; set; }
        public int SalesReturnCreditAdditionalCostId { get; set; }

    }

